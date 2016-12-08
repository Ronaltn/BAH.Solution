using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.Interaction;
using Kingdee.BOS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.DynamicForm
{
    public static class IOperationResultExtension
    {
        public static string GetErrorMessage(this IOperationResult result)
        {
            StringBuilder msg = new StringBuilder();
            result.ValidationErrors.ForEach(error => msg.AppendLine(string.Format("主键：{0}，错误：{1}", error.BillPKID, error.Message)));
            result.GetFatalErrorResults().ForEach(error => msg.AppendLine(string.Format("主键：{0}，错误：{1}", error.BillPKID, error.Message)));
            return msg.ToString();
        }//end static method

        public static string GetErrorMessage(this IOperationResult result, string description)
        {
            StringBuilder msg = new StringBuilder();
            result.ValidationErrors.ForEach(error => msg.AppendLine(string.Format("[{0}]主键：{1}，错误：{2}", description, error.BillPKID, error.Message)));
            result.GetFatalErrorResults().ForEach(error => msg.AppendLine(string.Format("[{0}]主键：{1}，错误：{2}", description, error.BillPKID, error.Message)));
            return msg.ToString();
        }//end static method

        public static string GetResultMessage(this IOperationResult result)
        {
            result.MergeValidateErrors();
            StringBuilder msg = new StringBuilder();
            if (result.InteractionContext != null && result.InteractionContext.Option.GetInteractionFlag().Any())
            {
                msg.AppendLine("因交互性提示而操作中断！");
            }
            foreach (var operate in result.OperateResult)
            {
                msg.AppendLine(operate.Message);
            }
            return msg.ToString();
        }//end static method

        public static void ThrowWhenUnSuccess(this IOperationResult result, Func<IOperationResult, string> predicate)
        {
            if (result.IsSuccess) return;

            string message = predicate(result);
            throw new KDBusinessException(string.Empty, message);
        }//end static method

    }//end static class
}//end namespace
