using Kingdee.BOS;
using Kingdee.BOS.Core.CommonFilter;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Report;
using Kingdee.BOS.Model.ReportFilter;
using Kingdee.BOS.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.Report
{
    public static class IRptParamsExtension
    {
        public static IRptParams CreateFromSysReportFilterScheme(this IRptParams rpt, Context ctx, FormMetadata reportMetadata, Action<ICommonFilterModelService> schemeSelector)
        {
            var reportFilterFormId = reportMetadata.BusinessInfo.GetForm().FilterObject;
            var reportFilterMetadata = FormMetaDataCache.GetCachedFormMetaData(ctx, reportFilterFormId);
            var filterMetadata = FormMetaDataCache.GetCachedFilterMetaData(ctx);//加载字段比较条件元数据。

            var reportFilterServiceProvider = reportFilterMetadata.BusinessInfo.GetForm().GetFormServiceProvider();

            var model = new SysReportFilterModel();
            model.SetContext(ctx, reportFilterMetadata.BusinessInfo, reportFilterServiceProvider);
            model.FormId = reportFilterMetadata.BusinessInfo.GetForm().Id;
            model.FilterObject.FilterMetaData = filterMetadata;
            model.InitFieldList(reportMetadata, reportFilterMetadata);
            model.GetSchemeList();

            schemeSelector = schemeSelector != null ? schemeSelector : new Action<ICommonFilterModelService>(s => { model.LoadDefaultScheme(); });
            schemeSelector(model);

            IRptParams p = new RptParams();
            p.FormId = reportFilterMetadata.BusinessInfo.GetForm().Id;
            p.StartRow = 1;
            p.EndRow = int.MaxValue;//StartRow和EndRow是报表数据分页的起始行数和截至行数，一般取所有数据，所以EndRow取int最大值。
            p.FilterParameter = model.GetFilterParameter();
            p.FilterFieldInfo = model.FilterFieldInfo;
            p.BaseDataTempTable.AddRange(PermissionServiceHelper.GetBaseDataTempTable(ctx, reportMetadata.BusinessInfo.GetForm().Id));

            return p;
        }//end static method

        public static IRptParams CreateFromSysReportFilterScheme(this IRptParams rpt, Context ctx, FormMetadata reportMetadata, string schemeId)
        {
            return CreateFromSysReportFilterScheme(rpt, ctx, reportMetadata, service => service.Load(schemeId));
        }//end static method

        public static IRptParams CreateFromSysReportFilterScheme(this IRptParams rpt, Context ctx, FormMetadata reportMetadata)
        {
            return CreateFromSysReportFilterScheme(rpt, ctx, reportMetadata, service => service.LoadDefaultScheme());
        }//end static method

    }//end static class
}//end namespace
