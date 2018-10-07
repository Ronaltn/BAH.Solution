using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.Util;
using Kingdee.BOS.VerificationHelper.Verifiers;
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

        public ServiceResult Invoke(string featureId = "")
        {
            var result = new ServiceResult<bool>();

            try
            {
                var ctx = this.KDContext.Session.AppContext;
                if (!featureId.IsNullOrEmptyOrWhiteSpace() && ctx != null)
                {
                    FeatureVerifier.CheckFeature(ctx, featureId);
                }//end if

                result.Code = (int)ResultCode.Success;
                result.Message = (ctx != null).ToString();
                result.Data = ctx != null;
            }
            catch (Exception ex)
            {
                result.Code = (int)ResultCode.Fail;
                result.Message = ex.Message;
            }

            return result;
        }//end method

    }//end class
}//end namespace
