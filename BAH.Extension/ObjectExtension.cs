using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Object对象的扩展类。
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 隐式转换。
        /// </summary>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <returns>返回结果。</returns>
        public static T ToType<T>(this object obj)
        {
            return (T)obj;
        }//end method

        /// <summary>
        /// 显式转换。
        /// </summary>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <returns>返回结果。</returns>
        public static T ToChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }//end method

        /// <summary>
        /// 隐式转换，匿名对象转换专用。
        /// </summary>
        /// <url>http://www.cnblogs.com/tianxiang2046/p/3586537.html</url>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <param name="t">匿名对象的初始化结构，例如：new{ Name = string.Empty, Age = 0 }。</param>
        /// <returns>返回结果。</returns>
        public static T ToAnonymousType<T>(this object obj, T t)
        {
            return (T)obj;
        }//end method

        /// <summary>
        /// 隐式转换，如果有异常则直接返回结果类型的默认值。
        /// </summary>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <returns>返回结果。</returns>
        public static T ToTypeOrDefault<T>(this object obj)
        {
            try
            {
                return ToType<T>(obj);
            }
            catch
            {
                return default(T);
            }
        }//end method

        /// <summary>
        /// 显式转换，如果有异常则直接返回结果类型的默认值。
        /// </summary>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <returns>返回结果。</returns>
        public static T ToChangeTypeOrDefault<T>(this object obj)
        {
            return ToChangeTypeOrDefault<T>(obj, default(T));
        }//end method

        /// <summary>
        /// 显式转换，如果有异常则直接返回指定的默认值。
        /// </summary>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <param name="def">指定的默认值。</param>
        /// <returns>返回结果。</returns>
        public static T ToChangeTypeOrDefault<T>(this object obj, T def)
        {
            try
            {
                return ToChangeType<T>(obj);
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// 隐式转换，如果有异常则直接返回结果类型的默认值，匿名对象转换专用。
        /// </summary>
        /// <url>http://www.cnblogs.com/tianxiang2046/p/3586537.html</url>
        /// <typeparam name="T">结果类型。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <param name="t">匿名对象的初始化结构，例如：new{ Name = string.Empty, Age = 0 }。</param>
        /// <returns>返回结果。</returns>
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
