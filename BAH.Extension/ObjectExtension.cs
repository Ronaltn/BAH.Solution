using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Objectd对象的扩展类。
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToType<T>(this object obj)
        {
            return (T)obj;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }//end method

        /// <summary>
        /// 匿名对象转换专用。
        /// </summary>
        /// <url>http://www.cnblogs.com/tianxiang2046/p/3586537.html</url>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="t">匿名对象的初始化结构，例如：new{ Name = string.Empty, Age = 0 }。</param>
        /// <returns></returns>
        public static T ToAnonymousType<T>(this object obj, T t)
        {
            return (T)obj;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="t">匿名对象的初始化结构，例如：new{ Name = string.Empty, Age = 0 }。</param>
        /// <returns></returns>
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
