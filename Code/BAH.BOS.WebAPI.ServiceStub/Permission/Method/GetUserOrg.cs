using BAH.BOS.WebAPI.ServiceStub.Permission.Dto;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub.Permission.Method
{
    public class GetUserOrg : KDBaseService
    {
        public GetUserOrg(KDServiceContext context) : base(context)
        {

        }

        public ServiceResult Invoke()
        {
            var result = new ServiceResult<List<UserOrgInfoOutput>>();

            try
            {
                var infos = PermissionServiceHelper.GetUserOrg(this.KDContext.Session.AppContext);

                result.Code = (int)ResultCode.Success;
                result.Message = ResultCode.Success.ToString();
                result.Data = infos.Select(db => new UserOrgInfoOutput
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
        }

    }//end class
}//end namespace
