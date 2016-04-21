using Kingdee.BOS.Orm.Metadata.DataEntity;
using Kingdee.BOS.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Orm.DataEntity
{
    public static class DynamicObjectCollectionExtension
    {
        public static DynamicObject[] LoadFromCache(this IEnumerable<DynamicObject> dataObject, Context ctx, string formId)
        {
            var type = FormMetaDataCache.GetCachedFormMetaData(ctx, formId).BusinessInfo.GetDynamicObjectType();
            return LoadFromCache(dataObject, ctx, type);
        }//end method

        public static DynamicObject[] LoadFromCache(this IEnumerable<DynamicObject> dataObject, Context ctx, DynamicObjectType type)
        {
            var pkArray = dataObject.Select(data => data.PkId()).ToArray();
            return BusinessDataServiceHelper.LoadFromCache(ctx, pkArray, type);
        }//end method

    }//end class
}//end namespace
