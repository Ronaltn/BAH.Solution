using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Metadata.EntityElement;
using Kingdee.BOS.Core.Metadata.FieldElement;
using Kingdee.BOS.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Orm.DataEntity
{
    public static class DynamicObjectExtension
    {
        #region 基础方法

        public static T Property<T>(this DynamicObject dataObject, string propertyName)
        {
            return dataObject == null || dataObject[propertyName] == null ? default(T) : dataObject[propertyName].ToType<T>();
        }//end method

        public static T FieldProperty<T>(this DynamicObject dataObject, BusinessInfo businessInfo, string keyName)
        {
            string propertyName = businessInfo.GetField(keyName).PropertyName;
            return Property<T>(dataObject, propertyName);
        }//end method

        public static T FieldProperty<T>(this DynamicObject dataObject, Field field)
        {
            return field.DynamicProperty.GetValue<T>(dataObject);
        }//end method

        public static DynamicObjectCollection EntryProperty(this DynamicObject dataObject, BusinessInfo businessInfo, string keyName)
        {
            string entryName = businessInfo.GetEntity(keyName).EntryName;
            return Property<DynamicObjectCollection>(dataObject, entryName);
        }//end method

        public static DynamicObjectCollection EntryProperty(this DynamicObject dataObject, Entity entity)
        {
            return entity.DynamicProperty.GetValue<DynamicObjectCollection>(dataObject);
        }//end method

        public static DynamicObject SubHeadProperty(this DynamicObject dataObject, BusinessInfo businessInfo, string keyName)
        {
            return EntryProperty(dataObject, businessInfo, keyName).FirstOrNullDefault();
        }//end method

        public static DynamicObject SubHeadProperty(this DynamicObject dataObject, Entity entity)
        {
            return EntryProperty(dataObject, entity).FirstOrNullDefault();
        }//end method

        public static DynamicObject LoadFromCache(this DynamicObject dataObject, Context ctx, string formId)
        {
            var type = FormMetaDataCache.GetCachedFormMetaData(ctx, formId).BusinessInfo.GetDynamicObjectType();
            var pkArray = new object[] { dataObject.PkId() };
            return BusinessDataServiceHelper.LoadFromCache(ctx, pkArray, type).FirstOrNullDefault();
        }//end method

        #endregion

        #region 主要元素

        public static object PkId(this DynamicObject dataObject)
        {
            return dataObject.Property<object>("Id");
        }//end method

        public static T PkId<T>(this DynamicObject dataObject)
        {
            return dataObject.PkId().ToChangeType<T>();
        }//end method

        public static int Seq(this DynamicObject dataObject)
        {
            return dataObject.Property<int>("Seq");
        }//end method

        #endregion

        #region 辅助资料

        public static string ADNumber(this DynamicObject dataObject)
        {
            return dataObject.Property<string>("FNumber");
        }//end method

        public static string ADName(this DynamicObject dataObject, Context ctx)
        {
            return ADName(dataObject, ctx.UserLocale.LCID);
        }//end method

        public static string ADName(this DynamicObject dataObject, int localeId)
        {
            return dataObject.Property<LocaleValue>("FDataValue").Value(localeId);
        }//end method

        #endregion

        #region 基础资料

        public static string BDNumber(this DynamicObject dataObject)
        {
            return dataObject.Property<string>("Number");
        }//end method

        public static string BDNumber(this DynamicObject dataObject, BaseDataField field)
        {
            return field.GetRefPropertyValue(dataObject, "FNumber").ToTypeOrDefault<string>();
        }//end method

        public static string BDName(this DynamicObject dataObject, int localeId)
        {
            return dataObject.Property<LocaleValue>("Name").Value(localeId);
        }//end method

        public static string BDName(this DynamicObject dataObject, BaseDataField field, int localeId)
        {
            return field.GetRefPropertyValue(dataObject, "FName").ToType<LocaleValue>().Value(localeId);
        }//end method

        public static string BDName(this DynamicObject dataObject, Context ctx)
        {
            return BDName(dataObject, ctx.UserLocale.LCID);
        }//end method

        public static string BDName(this DynamicObject dataObject, BaseDataField field, Context ctx)
        {
            return field.GetRefPropertyValue(dataObject, "FName").ToType<LocaleValue>().Value(ctx);
        }//end method

        public static string BillNo(this DynamicObject dataObject)
        {
            return dataObject.Property<string>("BillNo");
        }//end method

        public static string BillNo(this DynamicObject dataObject, Field field)
        {
            return field.DynamicProperty.GetValue<string>(dataObject);
        }//end method

        #endregion

    }//end class
}//end namespace
