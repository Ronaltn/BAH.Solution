using Kingdee.BOS;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Core;
using Kingdee.BOS.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS
{
    public static class ContextExtension
    {
        public static Context CreateAdministrator(this Context ctx, string dataCenterId)
        {
            var conf = KDConfiguration.Current;
            Context contextByDataCenterId = DataCenterService.GetDataCenterContextByID(dataCenterId);
            contextByDataCenterId.UserId = FormConst.AdministratorID;
            contextByDataCenterId.UserName = "Administrator";

            if (ctx == null) ctx = contextByDataCenterId;
            return contextByDataCenterId;
        }//end static method

        public static Context SetTimeZone(this Context ctx)
        {
            ILoginService loginService = null;
            try
            {
                loginService = ServiceFactory.GetLoginService(ctx.ServerUrl);
                loginService.SetContextTimeZone(ctx);
            }
            catch 
            {
                throw;
            }
            finally
            {
                ServiceFactory.CloseService(loginService);
            }

            return ctx;
        }//end static method

    }//end class
}//end namespace
