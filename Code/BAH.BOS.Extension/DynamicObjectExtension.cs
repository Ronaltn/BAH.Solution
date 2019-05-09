using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Const;
using Kingdee.BOS.Core.DynamicForm;
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

        public static T Property<T>(this DynamicObject dataObject, string propertyName, T defValue = default(T))
        {
            if (dataObject == null || dataObject[propertyName] == null || DBNull.Value.Equals(dataObject[propertyName]))
            {
                return defValue;
            }
            else if (typeof(T).Equals(typeof(DateTime?)))
            {
                return dataObject[propertyName].Adaptive(datetime => datetime != null ? datetime.ToType<DateTime>() : default(DateTime?))
                                               .Adaptive(datetime => datetime.HasValue && DateTime.Equals(datetime.Value, default(DateTime)) ? default(DateTime?) : datetime)
                                               .ToType<T>();
            }
            else if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {
                return dataObject[propertyName].ToChangeType<T>();
            }
            else
            {
                return dataObject[propertyName].ToType<T>();
            }
        }

        public static T FieldProperty<T>(this DynamicObject dataObject, BusinessInfo businessInfo, string keyName)
        {
            string propertyName = businessInfo.GetField(keyName).PropertyName;
            return Property<T>(dataObject, propertyName);
        }

        public static T FieldProperty<T>(this DynamicObject dataObject, Field field)
        {
            if (dataObject == null)
            {
                return default(T);
            }
            else if (typeof(T).Equals(typeof(DateTime?)))
            {
                return field.DynamicProperty.GetValue(dataObject)
                            .Adaptive(datetime => datetime != null ? datetime.ToType<DateTime>() : default(DateTime?))
                            .Adaptive(datetime => datetime.HasValue && DateTime.Equals(datetime.Value, default(DateTime)) ? default(DateTime?) : datetime)
                            .ToType<T>();
            }
            else if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {
                return field.DynamicProperty.GetValue(dataObject).ToChangeType<T>();
            }
            else
            {
                return field.DynamicProperty.GetValue(dataObject).ToType<T>();
            } 
        }

        public static T FieldRefIdProperty<T>(this DynamicObject dataObject, BaseDataField field)
        {
            if (dataObject == null)
            {
                return default(T);
            }
            else if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {
                return field.RefIDDynamicProperty.GetValue(dataObject).ToChangeType<T>();
            }
            else
            {
                return field.RefIDDynamicProperty.GetValue(dataObject).ToType<T>();
            } 
        }

        public static T FieldRefIdProperty<T>(this DynamicObject dataObject, RelatedFlexGroupField field)
        {
            if (dataObject == null)
            {
                return default(T);
            }
            else if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {
                return field.RefIDDynamicProperty.GetValue(dataObject).ToChangeType<T>();
            }
            else
            {
                return field.RefIDDynamicProperty.GetValue(dataObject).ToType<T>();
            }
        }

        public static T FieldRefProperty<T>(this DynamicObject dataObject, BaseDataField field, string keyName)
        {
            if (dataObject == null)
            {
                return default(T);
            }
            else if (typeof(T).Equals(typeof(DateTime?)))
            {
                return field.GetRefPropertyValue2(dataObject, keyName)
                            .Adaptive(datetime => datetime != null ? datetime.ToType<DateTime>() : default(DateTime?))
                            .ToType<T>();
            }
            else if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {
                return field.GetRefPropertyValue2(dataObject, keyName).ToChangeType<T>();
            }
            else
            {
                return field.GetRefPropertyValue2(dataObject, keyName).ToType<T>();
            }
        }

        public static DynamicObjectCollection EntryProperty(this DynamicObject dataObject, BusinessInfo businessInfo, string keyName)
        {
            string entryName = businessInfo.GetEntity(keyName).EntryName;
            return Property<DynamicObjectCollection>(dataObject, entryName);
        }

        public static DynamicObjectCollection EntryProperty(this DynamicObject dataObject, Entity entity)
        {
            return dataObject == null ? default(DynamicObjectCollection) : entity.DynamicProperty.GetValue<DynamicObjectCollection>(dataObject);
        }

        public static DynamicObject SubHeadProperty(this DynamicObject dataObject, BusinessInfo businessInfo, string keyName)
        {
            return EntryProperty(dataObject, businessInfo, keyName).FirstOrDefault();
        }

        public static DynamicObject SubHeadProperty(this DynamicObject dataObject, Entity entity)
        {
            return EntryProperty(dataObject, entity).FirstOrDefault();
        }

        #endregion

        #region 主要元素

        public static object PkId(this DynamicObject dataObject)
        {
            return dataObject.Property<object>(BOSConst.CST_KeyProperty);
        }//end method

        public static T PkId<T>(this DynamicObject dataObject)
        {
            return dataObject.Property<T>(BOSConst.CST_KeyProperty);
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
            return FieldRefProperty<string>(dataObject, field, field.NumberProperty.Key);
        }//end method

        public static LocaleValue BDName(this DynamicObject dataObject)
        {
            return dataObject.Property<LocaleValue>("Name");
        }

        public static string BDName(this DynamicObject dataObject, int localeId)
        {
            return BDName(dataObject).Value(localeId);
        }//end method

        public static string BDName(this DynamicObject dataObject, Context ctx)
        {
            return BDName(dataObject, ctx.UserLocale.LCID);
        }//end method

        public static string BDName(this DynamicObject dataObject, BaseDataField field)
        {
            return FieldRefProperty<string>(dataObject, field, field.NameProperty.Key);
        }//end method

        public static string BillNo(this DynamicObject dataObject)
        {
            return dataObject.Property<string>("BillNo");
        }//end method

        public static string BillNo(this DynamicObject dataObject, BillNoField field)
        {
            return FieldProperty<string>(dataObject, field);
        }//end method

        #endregion

        #region 功能方法

        public static DynamicObject AutoSetPrimaryKey(this DynamicObject dataObject, Context ctx)
        {
            return new DynamicObject[] { dataObject }.AutoSetPrimaryKey(ctx).FirstOrDefault();
        }//end method

        public static DynamicObject AutoSetBillNo(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo)
        {
            return new DynamicObject[] { dataObject }.AutoSetBillNo(ctx, businessInfo).FirstOrDefault();
        }//end method

        public static DynamicObject Load(this DynamicObject dataObject, Context ctx, DynamicObjectType type, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.Load(ctx, type, selector).FirstOrDefault();
        }//end method

        public static DynamicObject Load(this DynamicObject dataObject, Context ctx, string formId, Func<DynamicObject, object> selector = null, params string[] fieldKeys)
        {
            return new DynamicObject[] { dataObject }.Load(ctx, formId, selector, fieldKeys).FirstOrDefault();
        }

        public static DynamicObject LoadFromCache(this DynamicObject dataObject, Context ctx, DynamicObjectType type, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.LoadFromCache(ctx, type, selector).FirstOrDefault();
        }//end method

        public static DynamicObject LoadFromCache(this DynamicObject dataObject, Context ctx, string formId, Func<DynamicObject, object> selector = null, params string[] fieldKeys)
        {
            return new DynamicObject[] { dataObject }.LoadFromCache(ctx, formId, selector, fieldKeys).FirstOrDefault();
        }

        public static DynamicObject Append(this DynamicObject dataObject, string propertyName, Func<IEnumerable<DynamicObject>, DynamicObject[]> loader, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.Append(propertyName, loader, selector).FirstOrDefault();
        }

        public static DynamicObject Append(this DynamicObject dataObject, BaseDataField field, string keyName, Func<IEnumerable<DynamicObject>, DynamicObject[]> loader, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.Append(field, keyName, loader, selector).FirstOrDefault();
        }

        public static IOperationResult Draft(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null)
        {
            return BusinessDataServiceHelper.Draft(ctx, businessInfo, dataObject, option);
        }//end method

        public static DynamicObject Save(this DynamicObject dataObject, Context ctx)
        {
            return BusinessDataServiceHelper.Save(ctx, dataObject);
        }

        public static IOperationResult Save(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null)
        {
            return BusinessDataServiceHelper.Save(ctx, businessInfo, dataObject, option);
        }//end method

        public static void Delete(this DynamicObject dataObject, Context ctx, Func<DynamicObject, object> selector = null)
        {
            new DynamicObject[] { dataObject }.Delete(ctx, selector);
        }//end method

        public static IOperationResult Delete(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.Delete(ctx, businessInfo, option, selector);
        }//end method

        public static IOperationResult Submit(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.Submit(ctx, businessInfo, option, selector);
        }//end method

        public static IOperationResult CancelAssign(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.CancelAssign(ctx, businessInfo, option, selector);
        }//end method

        public static IOperationResult Audit(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.Audit(ctx, businessInfo, option, selector);
        }//end method

        public static IOperationResult UnAudit(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.UnAudit(ctx, businessInfo, option, selector);
        }//end method

        public static IOperationResult Enable(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.Enable(ctx, businessInfo, option, selector);
        }//end method

        public static IOperationResult Forbid(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            return new DynamicObject[] { dataObject }.Forbid(ctx, businessInfo, option, selector);
        }//end method

        public static IOperationResult DoNothing(this DynamicObject dataObject, Context ctx, BusinessInfo businessInfo, string operationNumber, OperateOption option = null)
        {
            return new DynamicObject[] { dataObject }.DoNothing(ctx, businessInfo, operationNumber, option);
        }//end static method

        #endregion

        #region 个性方法

        /// <summary>
        /// 向上钻去实体属性Key存在的动态数据对象。
        /// </summary>
        /// <param name="dataObject">动态数据对象。</param>
        /// <returns>返回钻取的数据包。</returns>
        public static DynamicObject Drill(this DynamicObject dataObject)
        {
            if (dataObject == null) return null;
            return dataObject.Parent == null ? dataObject : dataObject.Parent.AsType<DynamicObject>().Drill();
        }


        /// <summary>
        /// 向上钻去实体属性Key存在的动态数据对象。
        /// </summary>
        /// <param name="dataObject">动态数据对象。</param>
        /// <param name="propertyName">实体属性Key。</param>
        /// <returns>返回钻取的数据包。</returns>
        public static DynamicObject Drill(this DynamicObject dataObject, string propertyName)
        {
            if (dataObject == null) return null;
            return dataObject.DynamicObjectType.Properties.ContainsKey(propertyName) ? dataObject : dataObject.Parent.AsType<DynamicObject>().Drill(propertyName);
        }

        #endregion

    }//end class
}//end namespace
