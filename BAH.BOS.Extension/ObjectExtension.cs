using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ObjectExtension
    {
        public static T ToChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }//end method

        /// <summary>
        /// 匿名对象转换专用。
        /// </summary>
        /// <url>http://www.cnblogs.com/tianxiang2046/p/3586537.html</url>
        public static T ToAnonymousType<T>(this object obj, T t)
        {
            return (T)obj;
        }//end method

        public static T ToChangeTypeOrDefault<T>(this object obj)
        {
            try
            {
                return ToChangeType<T>(obj);
            }
            catch
            {
                return default(T);
            }
        }//end method

        /// <summary>
        /// 匿名对象转换专用。
        /// </summary>
        /// <url>http://www.cnblogs.com/tianxiang2046/p/3586537.html</url>
        public static T ToAnonymousTypeOrDefault<T>(this object obj, T t)
        {
            try
            {
                return ToAnonymousTypeOrDefault<T>(obj, t);
            }
            catch
            {
                return default(T);
            }
        }//end method
    }//end class
}//end namespace
