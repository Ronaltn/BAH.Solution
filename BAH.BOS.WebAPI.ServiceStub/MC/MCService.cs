using Kingdee.BOS;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.MC.ServiceFacade.KDServiceClient;
using Kingdee.BOS.WebApi.ServicesStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub.MC
{
    /// <summary>
    /// 管理中心服务。
    /// </summary>
    public class MCService : AbstractWebApiBusinessService
    {
        /// <summary>
        /// 获取业务数据中心。
        /// </summary>
        /// <returns>返回服务端结果。</returns>
        public virtual ServiceResult<List<DataCenter>> GetBusinessDataCenters()
        {
            var result = new ServiceResult<List<DataCenter>>();

            try
            {
                var murl = KDConfiguration.Current.ManagementSiteUrl;
                var service = ServiceFactory.GetDataCenterService(murl);
                var mctx = service.GetManagementDataCenterContext();
                var infos = service.GetAllBusinessDataCenterInfo(mctx);

                result.Code = (int)ResultCode.Success;
                result.Data = infos.Select(db => new DataCenter 
                {
                    Id = db.DataCenterID, 
                    Name = db.DataCenterName.ToString() 
                }).ToList();
            }
            catch
            {
                result.Code = (int)ResultCode.Fail;
            }

            return result;
        }//end method

    }//end class
}//end namespace
