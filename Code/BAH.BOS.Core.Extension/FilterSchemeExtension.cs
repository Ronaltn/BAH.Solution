using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Metadata.PreInsertData;
using Kingdee.BOS.Model.ListFilter;
using Kingdee.BOS.Serialization;
using Kingdee.BOS.ServiceHelper;
using Kingdee.BOS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.CommonFilter
{
    /// <url>
    /// http://club.kingdee.com/forum.php?mod=viewthread&tid=1159985&source=new_club&word=%E8%8E%B7%E5%8F%96%E8%BF%87%E6%BB%A4%E6%96%B9%E6%A1%88sql
    /// </url>
    public static class FilterSchemeExtension
    {
        public static string GetFilterString(this FilterScheme scheme, Context ctx, FormMetadata metadata)
        {
            if (scheme.Scheme.IsNullOrEmptyOrWhiteSpace())
            {
                return string.Empty;
            }

            //字段比较条件元数据。
            var filterMetadata = FormMetaDataCache.GetCachedFilterMetaData(ctx);

            //过滤模型：解析过滤方案
            ListFilterModel filterModel = new ListFilterModel();
            filterModel.FilterObject.FilterMetaData = filterMetadata;
            filterModel.SetContext(ctx, metadata.BusinessInfo, metadata.BusinessInfo.GetForm().GetFormServiceProvider());
            filterModel.InitFieldList(metadata, null);

            //把过滤方案的XML内容，反序列化为对象
            DcxmlSerializer dcxmlSerializer = new DcxmlSerializer(new PreInsertDataDcxmlBinder());
            SchemeEntity schemeEntity = dcxmlSerializer.DeserializeFromString(scheme.Scheme) as SchemeEntity;

            //利用ListFilterModel, 翻译过滤条件
            filterModel.FilterObject.Setting = schemeEntity.FilterSetting;
            string statement = filterModel.FilterObject.GetFilterSQLString(ctx, TimeServiceHelper.GetSystemDateTime(ctx));
            return statement;
        }//end metthod

    }//end class
}//end namespace
