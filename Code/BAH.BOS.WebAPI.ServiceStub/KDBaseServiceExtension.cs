using Kingdee.BOS.ServiceFacade.KDServiceFx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub
{
    /// <summary>
    /// API基类扩展。
    /// </summary>
    public static class KDBaseServiceExtension
    {
        /// <summary>
        /// 登录会话是否过期。
        /// </summary>
        /// <param name="service">API业务服务对象。</param>
        /// <param name="result">服务返回结果对象。</param>
        /// <param name="failedText">会话过期文本描述。</param>
        /// <returns>返回是否过期。</returns>
        public static bool IsContextExpired(this KDBaseService service, ServiceResult result, string failedText = "Context = null")
        {
            if (service.KDContext.Session.AppContext == null)
            {
                result.Code = (int)ResultCode.Fail;
                result.Message = failedText;
                return true;
            }
            else
            {
                return false;
            }
        }//end method

    }//end static class
}//end namespace
