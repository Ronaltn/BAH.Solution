using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAH.BOS.Pattern;

namespace BAH.BOS.Core.BillStatus
{
    /// <summary>
    /// 常用的单据状态。
    /// </summary>
    public class DocumentStatus : Singleton<DocumentStatus>
    {
        /// <summary>
        /// 暂存状态。
        /// </summary>
        /// <returns>返回状态值。returns>
        public string Draft()
        {
            return "Z";
        }

        /// <summary>
        /// 创建状态。
        /// </summary>
        /// <returns>返回状态值。</returns>
        public string Created()
        {
            return "A";
        }

        /// <summary>
        /// 审核中状态。
        /// </summary>
        /// <returns>返回状态值。</returns>
        public string Approving()
        {
            return "B";
        }

        /// <summary>
        /// 已审核状态。
        /// </summary>
        /// <returns>返回状态值。</returns>
        public string Approved()
        {
            return "C";
        }

        /// <summary>
        /// 重新审核状态。
        /// </summary>
        /// <returns>返回状态值。</returns>
        public string ReCreated()
        {
            return "D";
        }

    }//end class
}//end namespace
