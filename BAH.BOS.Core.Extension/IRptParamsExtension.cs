using Kingdee.BOS;
using Kingdee.BOS.Core.CommonFilter;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Report;
using Kingdee.BOS.Model.ReportFilter;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.Report
{
    public static class IRptParamsExtension
    {
        public static IRptParams CreateFromSysReportFilterScheme(this IRptParams rpt, Context ctx, FormMetadata reportMetadata, Func<ICommonFilterModelService, string> schemeSelector)
        {
            //字段比较条件元数据。
            var filterMetadata = FormMetaDataCache.GetCachedFilterMetaData(ctx);

            //账表元数据。
            var reportFormId = reportMetadata.BusinessInfo.GetForm().Id;

            //过滤条件元数据。
            var reportFilterFormId = reportMetadata.BusinessInfo.GetForm().FilterObject;
            var reportFilterMetadata = FormMetaDataCache.GetCachedFormMetaData(ctx, reportFilterFormId);
            var reportFilterServiceProvider = reportFilterMetadata.BusinessInfo.GetForm().GetFormServiceProvider();

            //过滤方案元数据。
            var schemeFormId = "BOS_FilterScheme";
            var schemeMetadata = FormMetaDataCache.GetCachedFormMetaData(ctx, schemeFormId);

            //用户参数元数据。
            var parameterDataFormId = reportMetadata.BusinessInfo.GetForm().ParameterObjectId;
            var parameterDataMetadata = FormMetaDataCache.GetCachedFormMetaData(ctx, parameterDataFormId);

            var model = new SysReportFilterModel();
            model.SetContext(ctx, reportFilterMetadata.BusinessInfo, reportFilterServiceProvider);
            model.FormId = reportFilterFormId;
            model.FilterObject.FilterMetaData = filterMetadata;
            model.InitFieldList(reportMetadata, reportFilterMetadata);
            model.GetSchemeList();
            
            //方案加载，返回选中的过滤方案主键。
            schemeSelector = schemeSelector != null ? schemeSelector : s => { model.LoadDefaultScheme(); return string.Empty; };
            var schemeId = schemeSelector(model);

            //打开参数，暂时不指定任何项。
            var openParameter = new Dictionary<string, object>();

            //如果指定了过滤方案，则根据过滤方案查找创建用户。
            long userId = -1L;
            if (!string.IsNullOrWhiteSpace(schemeId))
            {
                var schemeBusinessInfo = schemeMetadata.BusinessInfo.GetSubBusinessInfo(new List<string> { "FUserID" });
                userId = BusinessDataServiceHelper.LoadSingle(ctx, schemeId, schemeBusinessInfo.GetDynamicObjectType())
                                                  .FieldProperty<long>(schemeBusinessInfo.GetField("FUserID"));
            }//end if

            //加载用户参数数据包。
            var parameterData = UserParamterServiceHelper.Load(ctx, parameterDataMetadata.BusinessInfo, userId, reportFormId, KeyConst.USERPARAMETER_KEY);

            IRptParams p = new RptParams();
            p.CustomParams.Add(KeyConst.OPENPARAMETER_KEY, openParameter);
            p.FormId = reportFilterFormId;
            p.StartRow = 1;
            p.EndRow = int.MaxValue;//StartRow和EndRow是报表数据分页的起始行数和截至行数，一般取所有数据，所以EndRow取int最大值。
            p.FilterParameter = model.GetFilterParameter();
            p.FilterFieldInfo = model.FilterFieldInfo;
            p.BaseDataTempTable.AddRange(PermissionServiceHelper.GetBaseDataTempTable(ctx, reportFormId));
            p.ParameterData = parameterData;

            if (rpt == null) rpt = p;
            return p;
        }//end static method

        public static IRptParams CreateFromSysReportFilterScheme(this IRptParams rpt, Context ctx, FormMetadata reportMetadata, string schemeId)
        {
            return CreateFromSysReportFilterScheme(rpt, ctx, reportMetadata, service => { service.Load(schemeId); return schemeId; });
        }//end static method

        public static IRptParams CreateFromSysReportFilterScheme(this IRptParams rpt, Context ctx, FormMetadata reportMetadata)
        {
            return CreateFromSysReportFilterScheme(rpt, ctx, reportMetadata, service => { service.LoadDefaultScheme(); return string.Empty; });
        }//end static method

    }//end static class
}//end namespace
