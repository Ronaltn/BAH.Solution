using Kingdee.BOS;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.ServiceHelper;
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
        public MCService(KDServiceContext context)
            : base(context)
        {

        }//end constructor

        /// <summary>
        /// 获取业务数据中心。
        /// </summary>
        /// <returns>返回服务端结果。</returns>
        public virtual ServiceResult<List<object>> GetDataCenterList()
        {
            var result = new ServiceResult<List<object>>();

            try
            {
                var infos = DataCenterService.GetDataCentersFromMC(string.Empty, Context.DataBaseCategory.Normal);

                result.Code = (int)ResultCode.Success;
                result.Message = string.Format("成功返回{0}个数据中心！", infos.Count);
                result.Data = infos.Select(db => new
                {
                    Id = db.Id,
                    Number = db.Number,
                    Name = db.Name
                }).Cast<object>().ToList();
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
