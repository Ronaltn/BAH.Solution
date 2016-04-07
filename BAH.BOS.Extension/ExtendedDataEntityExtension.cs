using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Metadata.EntityElement;
using Kingdee.BOS.Core.Metadata.FieldElement;
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
            return dataObject.DataEntity.Property<T>(propertyName);
        }//end method

        public static T FieldProperty<T>(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            return dataObject.DataEntity.FieldProperty<T>(businessInfo, keyName);
        }//end method

        public static T FieldProperty<T>(this ExtendedDataEntity dataObject, Field field)
        {
            return dataObject.DataEntity.FieldProperty<T>(field);
        }//end method

        public static DynamicObjectCollection EntryProperty(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            return dataObject.DataEntity.EntryProperty(businessInfo, keyName);
        }//end method

        public static DynamicObjectCollection EntryProperty(this ExtendedDataEntity dataObject, Entity entity)
        {
            return dataObject.DataEntity.EntryProperty(entity);
        }//end method

        public static DynamicObject SubHeadProperty(this ExtendedDataEntity dataObject, BusinessInfo businessInfo, string keyName)
        {
            return dataObject.DataEntity.SubHeadProperty(businessInfo, keyName);
        }//end method

        public static DynamicObject SubHeadProperty(this ExtendedDataEntity dataObject, Entity entity)
        {
            return dataObject.DataEntity.SubHeadProperty(entity);
        }//end method

        #endregion

        #region 主要元素

        public static string PkId(this ExtendedDataEntity dataObject)
        {
            return dataObject.DataEntity.PkId();
        }//end method

        public static T PkId<T>(this ExtendedDataEntity dataObject)
        {
            return dataObject.DataEntity.PkId<T>();
        }//end method

        public static int Seq(this ExtendedDataEntity dataObject)
        {
            return dataObject.DataEntity.Seq();
        }//end method

        #endregion

    }//end class
}//end namespace
