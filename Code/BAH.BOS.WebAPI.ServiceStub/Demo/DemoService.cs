using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.WebApi.ServicesStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub.Demo
{
    /// <summary>
    /// 演示服务。
    /// </summary>
    public class DemoService : AbstractWebApiBusinessService
    {
        public DemoService(KDServiceContext context)
            : base(context)
        {

        }//end constructor

        public virtual ServiceResult HelloWorld()
        {
            return new ServiceResult
            {
                Code = (int)ResultCode.Success,
                Message = "Hello World"
            };
        }//end method

        public virtual ServiceResult HelloUser()
        {
            return new ServiceResult
            {
                Code = (int)ResultCode.Success,
                Message = string.Format("Hello {0}",this.KDContext.Session.AppContext.UserName)
            };
        }//end method

    }//end class
}//end namespace
