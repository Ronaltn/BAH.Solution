using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAH.BOS.Pattern;

namespace BAH.BOS.Core.BillStatus
{
    /// <summary>
    /// 常用的逻辑状态。
    /// </summary>
    public class LogicStatus : Singleton<LogicStatus>
    {
        /// <summary>
        /// 否状态。
        /// </summary>
        /// <returns>返回状态值。</returns>
        public string No()
        {
            return "A";
        }

        /// <summary>
        /// 是状态。
        /// </summary>
        /// <returns>返回状态值。</returns>
        public string Yes()
        {
            return "B";
        }

    }//end class
}//end namespace
