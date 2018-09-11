using BAH.BOS.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.Core.Const.FormOperation
{
    /// <summary>
    /// 上传操作选项。
    /// </summary>
    public class UploadOption : Singleton<UploadOption>
    {
        /// <summary>
        /// 截止操作。
        /// </summary>
        /// <returns>返回参数键。</returns>
        public string CutoffOperation()
        {
            return "_BAH_Cutoff_Operation_";
        }

        /// <summary>
        /// 在事务外执行。
        /// </summary>
        /// <returns>返回参数键。</returns>
        public string OutOfTransaction()
        {
            return "_BAH_OutOfTransaction_";
        }

    }//end class
}//end namespace
