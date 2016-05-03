using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace System
{
    /// <summary>
    /// GUID扩展类。
    /// </summary>
    public static class GuidExtension
    {
        /// <summary>
        /// 根据GUID获取16位的唯一字符串。
        /// </summary>
        /// <param name="guid">GUID。</param>
        /// <returns>返回字符串。</returns>
        public static string To16String(this Guid guid)
        {
            long i = 1;
            guid.ToByteArray().ToList().ForEach(b => i *= (b.ToType<int>() + 1));
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }//end method

        /// <summary>
        /// 根据GUID获取19位的唯一数字序列。
        /// </summary>
        /// <param name="guid">GUID。</param>
        /// <returns>返回字符串。</returns>
        public static long ToLongId(this Guid guid)
        {
            byte[] buffer = guid.ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }//end method

        /// <summary>
        /// 生成22位唯一的数字，并发可用。
        /// </summary>
        /// <param name="guid">GUID。</param>
        /// <returns>返回字符串。</returns>
        public static string ToUniqueId(this Guid guid)
        {
            Thread.Sleep(1); //保证yyyyMMddHHmmssffff唯一
            Random d = new Random(BitConverter.ToInt32(guid.ToByteArray(), 0));
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + d.Next(1000, 9999);
        }//end method

    }//end class
}//end namespace
