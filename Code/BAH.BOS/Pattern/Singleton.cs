using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BAH.BOS.Pattern
{
    /// <summary>
    /// 单例基类。
    /// </summary>
    /// <typeparam name="T">实例类型。</typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static readonly T instance = new T();

        /// <summary>
        /// 单例实例
        /// </summary>
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
