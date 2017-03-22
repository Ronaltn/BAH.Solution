using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.WebApi.ServicesStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub.User
{
    /// <summary>
    /// 用户服务。
    /// </summary>
    public class UserService : AbstractWebApiBusinessService
    {
        public UserService(KDServiceContext context)
            : base(context)
        { 
            
        }//end constructor

    }//end class
}//end namespace
