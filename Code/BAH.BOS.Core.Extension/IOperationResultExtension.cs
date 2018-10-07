using Kingdee.BOS.Core.Const;
using Kingdee.BOS.Core.Interaction;
using Kingdee.BOS.Orm;
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

        public static string GetResultMessage(this IOperationResult result, string separator = "\r\n")
        {
            result.MergeValidateErrors();
            StringBuilder msg = new StringBuilder();
            if (result.InteractionContext != null && result.InteractionContext.Option.GetInteractionFlag().Any())
            {
                var text = result.InteractionContext.SimpleMessage.IsNullOrEmptyOrWhiteSpace() ? "因交互性提示而操作中断！" : result.InteractionContext.SimpleMessage;
                msg.Append(text);
                msg.Append(separator);
            }
            /*
            foreach (var error in result.ValidationErrors)
            {
                msg.AppendLine(error.Message);
            }
            foreach (var error in result.GetFatalErrorResults())
            {
                msg.AppendLine(error.Message);
            }
            */
            foreach (var operate in result.OperateResult)
            {
                if (!operate.Message.IsNullOrEmptyOrWhiteSpace())
                {
                    msg.Append(operate.Message);
                    msg.Append(separator);
                }//end if
            }
            return msg.ToString();
        }//end static method

        public static void MergeFromOption(this IOperationResult result, OperateOption option)
        {
            var inner = option.GetVariableValue<IOperationResult>(BOSConst.CST_KEY_OperationResultKey);
            if (inner != null) result.MergeResult(inner);
        }//end static method

        public static void ThrowWhenUnSuccess(this IOperationResult result, Func<IOperationResult, string> predicate)
        {
            if (result.IsSuccess) return;

            string message = predicate(result);
            throw new KDBusinessException(string.Empty, message);
        }//end static method

        public static void ThrowWhenUnSuccess(this IOperationResult result, IOperationResult parent, Func<IOperationResult, string> predicate)
        {
            if (result.IsSuccess) return;
            if (result.InteractionContext != null && result.InteractionContext.Option.GetInteractionFlag().Any())
            {
                parent.InteractionContext = result.InteractionContext;
                parent.Sponsor = result.Sponsor;
            }//end if

            ThrowWhenUnSuccess(result, predicate);
        }//end static method

        public static void ThrowWhenUnSuccess(this IOperationResult result, bool needMessage = true)
        {
            ThrowWhenUnSuccess(result, op => needMessage ? op.GetResultMessage() : string.Empty);
        }//end static method

        public static void ThrowWhenInteraction(this IOperationResult result, bool interactive = true)
        {
            if (result != null && result.InteractionContext != null)
            {
                throw new KDInteractionException(result.InteractionContext.Option, result.Sponsor).Adaptive(ie =>
                {
                    ie.InteractionContext.Context = result.InteractionContext.Context;
                    ie.InteractionContext.DataEntities = result.InteractionContext.DataEntities;
                    ie.InteractionContext.FormShowParameter = result.InteractionContext.FormShowParameter;
                    ie.InteractionContext.InteractionFormId = result.InteractionContext.InteractionFormId;
                    ie.InteractionContext.IsInteractive = interactive;
                    ie.InteractionContext.K3DisplayerModel = result.InteractionContext.K3DisplayerModel;
                    ie.InteractionContext.SimpleMessage = result.InteractionContext.SimpleMessage;
                    ie.InteractionContext.SupportMobile = result.InteractionContext.SupportMobile;
                });
            }//end if
        }//end static method

        public static IOperationResult RepairPKValue(this IOperationResult result)
        {
            result.OperateResult
                  .Where(item => item.PKValueIsNullOrEmpty)
                  .Join(result.MapSuccessDataEnityIndex, left => left.DataEntityIndex, right => right.Value, (left, right) =>
                  {
                      left.PKValue = right.Key;
                      return left;
                  }).ToArray();
            return result;
        }//end static method

        public static void RemoveSuccessResult(this IOperationResult result)
        {
            var success = result.OperateResult.GetSuccessResult();
            foreach (var item in success) result.OperateResult.Remove(item);
        }//end static method

        public static void RemoveEmptyResult(this IOperationResult result)
        {
            var empty = result.OperateResult.Where(item => item.Message.IsNullOrEmptyOrWhiteSpace()).ToArray();
            foreach (var item in empty) result.OperateResult.Remove(item);
        }//end static method

    }//end static class
}//end namespace
