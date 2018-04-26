using BAH.BOS.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS
{
    /// <summary>
    /// 单例服务工厂。
    /// </summary>
    /// <typeparam name="T">抽象服务工厂。</typeparam>
    public class SingletonServiceFactory<T> : Singleton<T> where T : AbstractServiceFactory, new()
    {

    }//end class
}//end namespace
