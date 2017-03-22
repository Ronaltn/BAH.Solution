using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        /// <summary>
        /// 对象深拷贝。
        /// </summary>
        /// <typeparam name="T">对象泛型定义。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <returns>返回深拷贝后的对象。</returns>
        public static T DeepCopy<T>(this T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try 
                {
                    field.SetValue(retval, DeepCopy(field.GetValue(obj)));
                }
                catch { }
            }
            return (T)retval;
        }//end method

        /// <summary>
        /// 对象自适应。
        /// </summary>
        /// <typeparam name="T">对象泛型定义。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <param name="action">自适应的方法委托。</param>
        /// <returns>返回结果。</returns>
        public static T Adaptive<T>(this T obj, Action<T> action)
        {
            if (action != null) action(obj);
            return obj;
        }//end method

        /// <summary>
        /// 对象自适应。
        /// </summary>
        /// <typeparam name="TIn">输入对象泛型定义。</typeparam>
        /// <typeparam name="TOut">输出对象泛型定义。</typeparam>
        /// <param name="obj">Object对象。</param>
        /// <param name="precidate">自适应的方法委托。</param>
        /// <returns>返回结果。</returns>
        public static TOut Adaptive<TIn, TOut>(this TIn obj, Func<TIn, TOut> precidate)
        {
            return precidate == null ? default(TOut) : precidate(obj);
        }//end method

    }//end class
}//end namespace
