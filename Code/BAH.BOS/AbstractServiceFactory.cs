using Kingdee.BOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS
{
    /// <summary>
    /// 抽象服务工厂。
    /// </summary>
    public abstract class AbstractServiceFactory
    {
        #region 私有域

        /// <summary>
        /// 服务容器。
        /// </summary>
        private ServicesContainer mapServer = new ServicesContainer();

        /// <summary>
        /// 服务注册标记。
        /// </summary>
        private bool notRegistered = true;

        #endregion

        #region 公共属性

        /// <summary>
        /// 服务容器。
        /// </summary>
        public ServicesContainer MapServer
        {
            get
            {
                return this.mapServer;
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 从客户端获取服务接口实例。
        /// </summary>
        /// <typeparam name="T">接口类型。</typeparam>
        /// <param name="ctx">上下文对象。</param>
        /// <returns>返回服务实例。</returns>
        public virtual T GetService<T>(Context ctx)
        {
            this.InitlizeMapServer();
            return this.MapServer.GetService<T>(typeof(T), ctx.ServerUrl);
        }

        /// <summary>
        /// 从服务端获取服务接口实例。
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns>返回服务实例。</returns>
        public virtual T GetLocalService<T>()
        {
            this.InitlizeMapServer();
            return new ServicesContainer().GetService<T>(typeof(T).AssemblyQualifiedName, "");
        }

        /// <summary>
        /// 释放关闭服务。
        /// </summary>
        /// <param name="service">服务实例。</param>
        public void CloseService(object service)
        {
            IDisposable disposable = service as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// 初始化服务容器。
        /// </summary>
        protected void InitlizeMapServer()
        {
            if(notRegistered)
            {
                this.RegisterService();
            }//end if
            notRegistered = false;
        }

        /// <summary>
        /// 注册服务抽象方法。
        /// </summary>
        public abstract void RegisterService();

        #endregion

    }//end class
}//end namespace
