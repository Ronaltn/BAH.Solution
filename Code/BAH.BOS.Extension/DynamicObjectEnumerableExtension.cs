using Kingdee.BOS.Core;
using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.Interaction;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Metadata.FieldElement;
using Kingdee.BOS.Orm.Metadata.DataEntity;
using Kingdee.BOS.ServiceHelper;
using Kingdee.BOS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Orm.DataEntity
{
    public static class DynamicObjectEnumerableExtension
    {
        public static DynamicObject[] AutoSetPrimaryKey(this IEnumerable<DynamicObject> dataObject, Context ctx)
        {
            DynamicObject[] dataArray = dataObject.ToArray();
            if (dataArray.Any())
            {
                var group = dataArray.Select(data => new { DataEntity = data, DataType = data.DynamicObjectType })
                .GroupBy(a => a.DataType)
                .Select(g => new { DataType = g.Key, DataArray = g.Select(a => a.DataEntity).ToArray() })
                .ToList();
                group.ForEach(g => DBServiceHelper.AutoSetPrimaryKey(ctx, g.DataArray, g.DataType));
            }//end if
            return dataArray;
        }//end method

        public static DynamicObject[] AutoSetBillNo(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo,bool isUpdateMaxNum = true)
        {
            DynamicObject[] dataArray = dataObject.ToArray();
            if (dataArray.Any())
            {
                BusinessDataServiceHelper.GetBillNo(ctx, businessInfo, dataArray, isUpdateMaxNum);
            }//end if
            return dataArray;
        }//end method

        public static DynamicObject[] Load(this IEnumerable<DynamicObject> dataObject, Context ctx, DynamicObjectType type, Func<DynamicObject, object> selector = null)
        {
            var pkArray = selector != null ? dataObject.Select(selector).Distinct().ToArray() : dataObject.Select(data => data.PkId()).Distinct().ToArray();
            return BusinessDataServiceHelper.Load(ctx, pkArray, type);
        }//end method

        public static DynamicObject[] Load(this IEnumerable<DynamicObject> dataObject, Context ctx, string formId, Func<DynamicObject, object> selector = null, params string[] fieldKeys)
        {
            var metadata = FormMetaDataCache.GetCachedFormMetaData(ctx, formId);
            var businessInfo = metadata.BusinessInfo.Adaptive(info =>
            {
                if (fieldKeys != null && fieldKeys.Length > 0)
                {
                    return info.GetSubBusinessInfo(fieldKeys.ToList());
                }
                else
                {
                    return info;
                }
            });
            return Load(dataObject, ctx, businessInfo.GetDynamicObjectType(), selector);
        }//end method

        public static DynamicObject[] LoadFromCache(this IEnumerable<DynamicObject> dataObject, Context ctx, DynamicObjectType type, Func<DynamicObject, object> selector = null)
        {
            var pkArray = selector != null ? dataObject.Select(selector).Distinct().ToArray() : dataObject.Select(data => data.PkId()).Distinct().ToArray();
            return BusinessDataServiceHelper.LoadFromCache(ctx, pkArray, type);
        }//end method

        public static DynamicObject[] LoadFromCache(this IEnumerable<DynamicObject> dataObject, Context ctx, string formId, Func<DynamicObject, object> selector = null, params string[] fieldKeys)
        {
            var metadata = FormMetaDataCache.GetCachedFormMetaData(ctx, formId);
            var businessInfo = metadata.BusinessInfo.Adaptive(info => 
            {
                if (fieldKeys != null && fieldKeys.Length > 0)
                {
                    return info.GetSubBusinessInfo(fieldKeys.ToList());
                }
                else
                {
                    return info;
                }
            });
            return LoadFromCache(dataObject, ctx, businessInfo.GetDynamicObjectType(), selector);
        }//end method

        public static DynamicObject[] Append(this IEnumerable<DynamicObject> dataObject, string propertyName, Func<IEnumerable<DynamicObject>, DynamicObject[]> loader, Func<DynamicObject, object> selector = null)
        {
            var objs = dataObject.Select(data => data.Property<DynamicObject>(propertyName)).Where(data => data != null).ToArray();
            var bag = objs.Any() ? loader(objs) : new DynamicObject[0];
            return dataObject.Join(bag,
                                   left => selector != null ? selector(left.Property<DynamicObject>(propertyName)) : left.Property<DynamicObject>(propertyName).PkId(),
                                   right => selector != null ? selector(right) : right.PkId(),
                                   (left, right) =>
                                   {
                                       left[propertyName] = right;
                                       return left;
                                   }).ToArray();
        }//end method

        public static DynamicObject[] Append(this IEnumerable<DynamicObject> dataObject, BaseDataField field, string keyName, Func<IEnumerable<DynamicObject>, DynamicObject[]> loader, Func<DynamicObject, object> selector = null)
        {
            var propertyName = field.RefProperties.Where(p => p.Key.EqualsIgnoreCase(keyName)).FirstOrDefault().PropertyName;
            return Append(dataObject, propertyName, loader, selector);
        }//end method

        public static DynamicObject[] Mend(this IEnumerable<DynamicObject> dataObject, BaseDataField field, Func<object[], DynamicObject[]> loader, Func<DynamicObject, object> selector = null)
        {
            var ids = dataObject.Select(data => data.FieldRefIdProperty<object>(field)).Where(id => id != null).Distinct().ToArray();
            var bag = ids.Any() ? loader(ids) : new DynamicObject[0];
            return dataObject.Join(bag,
                                   left => left.FieldRefIdProperty<object>(field),
                                   right => selector != null ? selector(right) : right.PkId(),
                                   (left, right) =>
                                   {
                                       left[field.PropertyName] = right;
                                       return left;
                                   }).ToArray();
        }

        public static DynamicObject[] Mend(this IEnumerable<DynamicObject> dataObject, BaseDataField field, Func<DynamicObject, object> selfSelector, Func<DynamicObject, object> loadSelector, Func<object[], DynamicObject[]> loader)
        {
            var traits = dataObject.Select(data => selfSelector(data)).Where(item => item != null).Distinct().ToArray();
            var bag = traits.Any() ? loader(traits) : new DynamicObject[0];
            return dataObject.Join(bag,
                                   left => selfSelector(left),
                                   right => loadSelector(right),
                                   (left, right) =>
                                   {
                                       left[field.PropertyName] = right;
                                       return left;
                                   }).ToArray();
        }

        public static IOperationResult Draft(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            IOperationResult result = BusinessDataServiceHelper.Draft(ctx, businessInfo, dataObject.ToArray(), option);
            return result;
        }//end method

        public static DynamicObject[] Save(this IEnumerable<DynamicObject> dataObject, Context ctx)
        {
            return BusinessDataServiceHelper.Save(ctx, dataObject.ToArray());
        }//end method

        public static IOperationResult Save(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            IOperationResult result = BusinessDataServiceHelper.Save(ctx, businessInfo, dataObject.ToArray(), option);
            return result;
        }//end method

        public static void Delete(this IEnumerable<DynamicObject> dataObject, Context ctx, Func<DynamicObject, object> selector = null)
        {
            DynamicObject[] dataArray = dataObject.ToArray();
            if (dataArray.Any())
            {
                var group = dataArray.Select(data => new { DataEntity = data, DataType = data.DynamicObjectType })
                .GroupBy(a => a.DataType)
                .Select(g => new { DataType = g.Key, Ids = g.Select(a => a.DataEntity).Select(data => selector != null ? selector(data) : data.PkId()).ToArray() })
                .ToList();

                group.ForEach(g => BusinessDataServiceHelper.Delete(ctx, g.Ids, g.DataType));
            }//end if
        }//end method

        public static IOperationResult Delete(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo,OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            object[] pkIds = selector != null ? dataObject.Select(selector).ToArray() : dataObject.Select(data => data.PkId()).ToArray();
            IOperationResult result = BusinessDataServiceHelper.Delete(ctx, businessInfo, pkIds, option, OperationNumberConst.OperationNumber_Delete);
            return result;
        }//end method

        public static IOperationResult Submit(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            object[] pkIds = selector != null ? dataObject.Select(selector).ToArray() : dataObject.Select(data => data.PkId()).ToArray();
            IOperationResult result = BusinessDataServiceHelper.Submit(ctx, businessInfo, pkIds, "Submit", option);
            return result;
        }//end method

        public static IOperationResult CancelAssign(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            var pkIds = dataObject.Select(data => new KeyValuePair<object, object>(selector != null ? selector(data) : data.PkId(), ""))
                                  .ToList();

            IOperationResult result = BusinessDataServiceHelper.SetBillStatus(ctx, businessInfo, pkIds, null, OperationNumberConst.OperationNumber_CancelAssign, option);
            return result;
        }//end method

        public static IOperationResult Audit(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            var pkIds = dataObject.Select(data => new KeyValuePair<object, object>(selector != null ? selector(data) : data.PkId(), ""))
                                  .ToList();

            List<object> paraAudit = new List<object>();
            paraAudit.Add("1");//表示审核动作。
            paraAudit.Add("");//表示审核意见。

            IOperationResult result = BusinessDataServiceHelper.SetBillStatus(ctx, businessInfo, pkIds, paraAudit, OperationNumberConst.OperationNumber_Audit, option);
            return result;
        }//end method

        public static IOperationResult UnAudit(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            var pkIds = dataObject.Select(data => new KeyValuePair<object, object>(selector != null ? selector(data) : data.PkId(), ""))
                                  .ToList();

            List<object> paraUnAudit = new List<object>();
            paraUnAudit.Add("2");//表示反审核动作。
            paraUnAudit.Add("");//表示反审核意见。

            IOperationResult result = BusinessDataServiceHelper.SetBillStatus(ctx, businessInfo, pkIds, paraUnAudit, OperationNumberConst.OperationNumber_UnAudit, option);
            return result;
        }//end method

        public static IOperationResult Enable(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            var pkIds = dataObject.Select(data => new KeyValuePair<object, object>(selector != null ? selector(data) : data.PkId(), ""))
                                  .ToList();

            IOperationResult result = BusinessDataServiceHelper.SetBillStatus(ctx, businessInfo, pkIds, null, OperationNumberConst.OperationNumber_Enable, option);
            return result;
        }//end method

        public static IOperationResult Forbid(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            var pkIds = dataObject.Select(data => new KeyValuePair<object, object>(selector != null ? selector(data) : data.PkId(), ""))
                                  .ToList();

            IOperationResult result = BusinessDataServiceHelper.SetBillStatus(ctx, businessInfo, pkIds, null, OperationNumberConst.OperationNumber_Forbid, option);
            return result;
        }//end method

        public static IOperationResult DoNothing(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, string operationNumber, OperateOption option = null)
        {
            if (option == null) option = OperateOption.Create();
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);

            IOperationResult result = BusinessDataServiceHelper.DoNothingWithDataEntity(ctx, businessInfo, dataObject.ToArray(), operationNumber, option);
            return result;
        }//end method

    }//end class
}//end namespace
