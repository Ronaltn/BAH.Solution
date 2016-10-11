using Kingdee.BOS.Orm.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Orm.DataEntity
{
    public static class IDataEntityBaseExtension
    {
        public static object Clone(this IDataEntityBase dataEntity, bool clearPrimaryKeyValue = true)
        {
            return OrmUtils.Clone(dataEntity, clearPrimaryKeyValue);
        }//end method

        public static object PrimaryKeyValue(this IDataEntityBase dataEntity)
        {
            return OrmUtils.GetPrimaryKeyValue(dataEntity, false);
        }//end method

        public static T PrimaryKeyValue<T>(this IDataEntityBase dataEntity)
        {
            return OrmUtils.GetPrimaryKeyValue<T>(dataEntity, false);
        }//end method

    }//end class
}//end namespace
