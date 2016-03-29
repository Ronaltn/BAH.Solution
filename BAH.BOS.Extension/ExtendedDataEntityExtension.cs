using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Orm.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core
{
    public static class ExtendedDataEntityExtension
    {
        public static T Property<T>(this ExtendedDataEntity dataObject, string propertyName)
        {
            if (dataObject[propertyName] == null)
            {
                return default(T);
            }
            else
            {
                return dataObject[propertyName].ToType<T>();
            }
        }//end method

        public static T FieldProperty<T>(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            return Property<T>(dataObject, businessInfo.GetField(keyName).PropertyName);
        }//end method

        public static DynamicObject SubHeadProperty(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            return EntryProperty(dataObject, businessInfo, keyName).FirstOrNullDefault();
        }//end method

        public static DynamicObjectCollection EntryProperty(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            string entryName = businessInfo.GetEntity(keyName).EntryName;
            return Property<DynamicObjectCollection>(dataObject, entryName);
        }//end method

    }//end class
}//end namespace
