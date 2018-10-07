using BAH.BOS.WebAPI.ServiceStub.MC.Dto;
using Kingdee.BOS;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.ServiceHelper;
using Kingdee.BOS.Util;
using Kingdee.BOS.VerificationHelper.Verifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub.MC.Method
{
    /// <summary>
    /// 获取业务数据中心列表。
    /// </summary>
    public class GetDataCenterList : KDBaseService
    {
        public GetDataCenterList(KDServiceContext context) : base(context)
        {

        }

        public virtual ServiceResult Invoke(string featureId = "")
        {
            var result = new ServiceResult<List<DataCenterInfoOutput>>();

            try
            {
                var infos = DataCenterService.GetDataCentersFromMC(string.Empty, Context.DataBaseCategory.Normal);
                if (!featureId.IsNullOrEmptyOrWhiteSpace() && infos.Any())
                {
                    var ctx = default(Context);
                    ctx = ctx.CreateAdministratorFromCache(infos.First().Id);
                    FeatureVerifier.CheckFeature(ctx, featureId);
                }//end if

                result.Code = (int)ResultCode.Success;
                result.Message = ResultCode.Success.ToString();
                result.Data = infos.Select(db => new DataCenterInfoOutput
                {
                    Id = db.Id,
                    Number = db.Number,
                    Name = db.Name
                }).ToList();
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
