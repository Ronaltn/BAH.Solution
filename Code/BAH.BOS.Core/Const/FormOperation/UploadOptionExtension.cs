using BAH.BOS.Core.Const.FormOperation;
using Kingdee.BOS.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.Core.Const.FormOperation
{
    /// <summary>
    /// 上传操作相关选项。
    /// </summary>
    public static class UploadOptionExtension
    {
        /// <summary>
        /// 获取截止的操作编码。
        /// </summary>
        /// <param name="option">选项对象。</param>
        /// <returns>返回操作编码。</returns>
        public static string GetCutoffOperation(this OperateOption option)
        {
            string number = string.Empty;
            option.TryGetVariableValue<string>(UploadOption.Instance.CutoffOperation(), out number);
            return number;
        }

        /// <summary>
        /// 设置截止的操作编码。
        /// </summary>
        /// <param name="option">选项对象。</param>
        public static void SetCutoffOperation(this OperateOption option, string number)
        {
            option.SetVariableValue(UploadOption.Instance.CutoffOperation(), number);
        }

        /// <summary>
        /// 获取是否在事务外执行的逻辑值。
        /// </summary>
        /// <param name="option">选项对象。</param>
        /// <returns>返回逻辑值。</returns>
        public static bool GetOutOfTransaction(this OperateOption option)
        {
            bool logic = default(bool);
            option.TryGetVariableValue<bool>(UploadOption.Instance.OutOfTransaction(), out logic);
            return logic;
        }

        /// <summary>
        /// 设置是否在事务外执行的逻辑值。
        /// </summary>
        /// <param name="option">选项对象。</param>
        /// <returns>返回逻辑值。</returns>
        public static void SetOutOfTransaction(this OperateOption option, bool logic)
        {
            option.SetVariableValue(UploadOption.Instance.OutOfTransaction(), logic);
        }

        /// <summary>
        /// 获取失败时抛出异常的逻辑值。
        /// </summary>
        /// <param name="option">选项对象。</param>
        /// <returns>返回逻辑值。</returns>
        public static bool GetThrowWhenUnSuccess(this OperateOption option)
        {
            bool logic = true;
            option.TryGetVariableValue(UploadOption.Instance.ThrowWhenUnSuccess(), out logic);
            return logic;
        }

        /// <summary>
        /// 设置失败时抛出异常的逻辑值。
        /// </summary>
        /// <param name="option">选项对象。</param>
        /// <returns>返回逻辑值。</returns>
        public static void SetThrowWhenUnSuccess(this OperateOption option, bool logic)
        {
            option.SetVariableValue(UploadOption.Instance.ThrowWhenUnSuccess(), logic);
        }
    }
}
