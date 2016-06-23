using BAH.BOS.WebAPI.ServiceStub.BusinessOperationResult;
using Kingdee.BOS;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.WebApi.ServicesStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub
{
    /// <summary>
    /// 会话服务。
    /// </summary>
    public class SessionService : AbstractWebApiBusinessService
    {
        public SessionService(KDServiceContext context)
            : base(context)
        { 
            
        }//end constructor

        /// <summary>
        /// 接收心跳信号，返回服务端结果。
        /// </summary>
        /// <returns>返回服务端结果。</returns>
        public virtual ServiceResult Heartbeat()
        {
            return new ServiceResult
            {
                Code = (int)ResultCode.Success,
                Message = string.Format("SessionId:{0}，服务端已收到你的心跳信号！", this.KDContext.Session.AppContext.SessionId)
            };
        }//end method

    }//end class
}//end namespace
