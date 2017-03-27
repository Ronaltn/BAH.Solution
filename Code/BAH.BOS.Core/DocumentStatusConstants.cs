using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.Core
{
    /// <summary>
    /// 单据状态常量。
    /// </summary>
    public static class DocumentStatusConstants
    {
        /// <summary>
        /// 暂存。
        /// </summary>
        public static readonly string Draft = "Z";

        /// <summary>
        /// 创建。
        /// </summary>
        public static readonly string Created = "A";

        /// <summary>
        /// 审核中。
        /// </summary>
        public static readonly string Approving = "B";

        /// <summary>
        /// 已审核。
        /// </summary>
        public static readonly string Approved = "C";

        /// <summary>
        /// 重新审核。
        /// </summary>
        public static readonly string ReCreated = "D";
    }
}
