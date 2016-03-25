using Kingdee.BOS.Core.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Orm.DataEntity
{
    public static class DynamicObjectExtension
    {
        public static T Property<T>(this DynamicObject dataObject, string propertyName)
        {
            if (dataObject == null || dataObject[propertyName] == null)
            {
                return default(T);
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

        public static DynamicObjectCollection EntryProperty(this DynamicObject dataObject, BusinessInfo businessInfo, string keyName)
        {
            string entryName = businessInfo.GetEntity(keyName).EntryName;
            return Property<DynamicObjectCollection>(dataObject, entryName);
        }//end method

        public static T PkId<T>(this DynamicObject dataObject)
        {
            return dataObject.Property<T>("Id");
        }//end method

        public static int Seq(this DynamicObject dataObject)
        {
            return dataObject.Property<int>("Seq");
        }//end method

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

        public static string BDNumber(this DynamicObject dataObject)
        {
            return dataObject.Property<string>("Number");
        }//end method

        public static string BDName(this DynamicObject dataObject, int localeId)
        {
            return dataObject.Property<LocaleValue>("Name").Value(localeId);
        }//end method

        public static string BDName(this DynamicObject dataObject, Context ctx)
        {
            return BDName(dataObject, ctx.UserLocale.LCID);
        }//end method

        public static string BillNo(this DynamicObject dataObject)
        {
            return dataObject.Property<string>("BillNo");
        }//end method

    }//end class
}//end namespace
