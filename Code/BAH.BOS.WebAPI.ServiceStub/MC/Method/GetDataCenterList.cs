using BAH.BOS.WebAPI.ServiceStub.MC.Dto;
using Kingdee.BOS;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.ServiceHelper;
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

        public virtual ServiceResult Invoke()
        {
            var result = new ServiceResult<List<DataCenterInfoOutput>>();

            try
            {
                var infos = DataCenterService.GetDataCentersFromMC(string.Empty, Context.DataBaseCategory.Normal);

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
