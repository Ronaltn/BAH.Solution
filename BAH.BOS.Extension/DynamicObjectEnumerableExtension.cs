using Kingdee.BOS.App.Core;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Orm.Metadata.DataEntity;
using Kingdee.BOS.ServiceHelper;
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

                SequenceReader reader = new SequenceReader(ctx);
                group.ForEach(g => reader.AutoSetPrimaryKey(g.DataArray, g.DataType));
            }//end if
            return dataArray;
        }//end method

        public static DynamicObject[] AutoSetBillNo(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo)
        {
            DynamicObject[] dataArray = dataObject.ToArray();
            if (dataArray.Any())
            {
                BusinessDataServiceHelper.GetBillNo(ctx, businessInfo, dataArray, true);
            }//end if
            return dataArray;
        }//end method

        public static DynamicObject[] LoadFromCache(this IEnumerable<DynamicObject> dataObject, Context ctx, DynamicObjectType type, Func<DynamicObject, object> selector = null)
        {
            var pkArray = selector != null ? dataObject.Select(selector).ToArray() : dataObject.Select(data => data.PkId()).ToArray();
            return BusinessDataServiceHelper.LoadFromCache(ctx, pkArray, type);
        }//end method

        public static IOperationResult Draft(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null)
        {
            IOperationResult result = BusinessDataServiceHelper.Draft(ctx, businessInfo, dataObject.ToArray(), option);
            return result;
        }//end method

        public static IOperationResult Save(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null)
        {
            IOperationResult result = BusinessDataServiceHelper.Save(ctx, businessInfo, dataObject.ToArray(), option);
            return result;
        }//end method

        public static IOperationResult Submit(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            object[] pkIds = selector != null ? dataObject.Select(selector).ToArray() : dataObject.Select(data => data.PkId()).ToArray();
            IOperationResult result = BusinessDataServiceHelper.Submit(ctx, businessInfo, pkIds, "Submit", option);
            return result;
        }//end method

        public static IOperationResult Audit(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, OperateOption option = null, Func<DynamicObject, object> selector = null)
        {
            var pkIds = dataObject.Select(data => new KeyValuePair<object, object>(selector != null ? selector(data) : data.PkId(), ""))
                                  .ToList();

            List<object> paraAudit = new List<object>();
            paraAudit.Add("1");//表示审核动作。
            paraAudit.Add("");//表示审核意见。

            IOperationResult result = BusinessDataServiceHelper.SetBillStatus(ctx, businessInfo, pkIds, paraAudit, OperationNumberConst.OperationNumber_Audit, option);
            return result;
        }//end method

        public static IOperationResult DoNothing(this IEnumerable<DynamicObject> dataObject, Context ctx, BusinessInfo businessInfo, string operationNumber, OperateOption option = null)
        {
            IOperationResult result = BusinessDataServiceHelper.DoNothingWithDataEntity(ctx, businessInfo, dataObject.ToArray(), operationNumber, option);
            return result;
        }

    }//end class
}//end namespace
