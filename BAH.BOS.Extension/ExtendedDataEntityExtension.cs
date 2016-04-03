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
        #region 基础方法

        public static T Property<T>(this ExtendedDataEntity dataObject, string propertyName)
        {
            return dataObject[propertyName] == null ? default(T) : dataObject[propertyName].ToType<T>();
        }//end method

        public static T FieldProperty<T>(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            string propertyName = businessInfo.GetField(keyName).PropertyName;
            return Property<T>(dataObject, propertyName);
        }//end method

        public static DynamicObjectCollection EntryProperty(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            string entryName = businessInfo.GetEntity(keyName).EntryName;
            return Property<DynamicObjectCollection>(dataObject, entryName);
        }//end method

        public static DynamicObject SubHeadProperty(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            return EntryProperty(dataObject, businessInfo, keyName).FirstOrNullDefault();
        }//end method

        #endregion

        #region 主要元素

        public static T PkId<T>(this ExtendedDataEntity dataObject)
        {
            return dataObject.Property<T>("Id");
        }//end method

        public static int Seq(this ExtendedDataEntity dataObject)
        {
            return dataObject.Property<int>("Seq");
        }//end method

        #endregion

    }//end class
}//end namespace
