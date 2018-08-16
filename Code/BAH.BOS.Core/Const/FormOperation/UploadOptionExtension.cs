using BAH.BOS.Core.Const.FormOperation;
using Kingdee.BOS.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.App.ServicePlugIn.FormOperation
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
    }
}
