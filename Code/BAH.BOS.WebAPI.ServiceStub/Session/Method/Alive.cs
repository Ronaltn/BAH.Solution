using Kingdee.BOS.ServiceFacade.KDServiceFx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub.Session.Method
{
    /// <summary>
    /// 检测登录会话是否有效。
    /// </summary>
    public class Alive : KDBaseService
    {
        public Alive(KDServiceContext context) : base(context)
        {

        }

        public ServiceResult Invoke()
        {
            return new ServiceResult<bool>
            {
                Code = (int)ResultCode.Success,
                Message = (this.KDContext.Session.AppContext != null).ToString(),
                Data = this.KDContext.Session.AppContext != null
            };
        }

    }//end class
}//end namespace
