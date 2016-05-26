using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Metadata.EntityElement;
using Kingdee.BOS.Core.Metadata.FieldElement;
using Kingdee.BOS.Orm;
using Kingdee.BOS.Orm.Metadata.DataEntity;
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
            if (dataObject == null || dataObject[propertyName] == null || DBNull.Value.Equals(dataObject[propertyName]))
            {
                return default(T);
            }
            else if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {
                return dataObject[propertyName].ToChangeType<T>();
            }
            else
            {
                return dataObject[propertyName].ToType<T>();
            }

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

        public static T FieldRefProperty<T>(this DynamicObject dataObject, BaseDataField field, string keyName)
        {
            return field.GetRefPropertyValue(dataObject, keyName).ToType<T>();
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
            return EntryProperty(dataObject, entity).SingleOrNullDefault();
        }//end method

        #endregion

        #region 主要元素

        public static object PkId(this DynamicObject dataObject)
        {
            return dataObject.Property<object>("Id");
        }//end method

        public static T PkId<T>(this DynamicObject dataObject)
        {
            return dataObject.Property<T>("Id");
        }//end method

        public static object MasterId(this DynamicObject dataObject)
        {
            return dataObject.Property<object>(FormConst.MASTER_ID);
        }//end method

        public static T MasterId<T>(this DynamicObject dataObject)
        {
            return dataObject.Property<T>(FormConst.MASTER_ID);
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
            return FieldRefProperty<string>(dataObject, field, "FNumber");
        }//end method

        public static string BDName(this DynamicObject dataObject, int localeId)
        {
            return dataObject.Property<LocaleValue>("Name").Value(localeId);
        }//end method

        public static string BDName(this DynamicObject dataObject, Context ctx)
        {
            return BDName(dataObject, ctx.UserLocale.LCID);
        }//end method

        public static string BDName(this DynamicObject dataObject, BaseDataField field)
        {
            return FieldRefProperty<string>(dataObject, field, "FName");
        }//end method

        public static string BillNo(this DynamicObject dataObject)
        {
            return dataObject.Property<string>("BillNo");
        }//end method

        public static string BillNo(this DynamicObject dataObject, BillNoField field)
        {
            return field.DynamicProperty.GetValue<string>(dataObject);
        }//end method

        #endregion

        #region 功能方法

        public static DynamicObject Duplicate(this DynamicObject dataObject, bool clearPrimaryKeyValue = true)
        {
            return OrmUtils.Clone(dataEntity: dataObject, clearPrimaryKeyValue: clearPrimaryKeyValue).ToType<DynamicObject>();
        }//end method

        public static DynamicObject LoadFromCache(this DynamicObject dataObject, Context ctx, string formId)
        {
            var type = FormMetaDataCache.GetCachedFormMetaData(ctx, formId).BusinessInfo.GetDynamicObjectType();
            return LoadFromCache(dataObject, ctx, type, data => data.PkId());
        }//end method

        public static DynamicObject LoadFromCache(this DynamicObject dataObject, Context ctx, DynamicObjectType type)
        {
            return LoadFromCache(dataObject, ctx, type, data => data.PkId());
        }//end method

        public static DynamicObject LoadFromCache(this DynamicObject dataObject, Context ctx, DynamicObjectType type, Func<DynamicObject, object> selector)
        {
            object pkId = selector != null ? selector(dataObject) : default(object);
            var pkArray = new object[] { pkId };
            return BusinessDataServiceHelper.LoadFromCache(ctx, pkArray, type).FirstOrNullDefault();
        }//end method

        #endregion

    }//end class
}//end namespace
