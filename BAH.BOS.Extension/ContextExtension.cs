using Kingdee.BOS;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS
{
    public static class ContextExtension
    {
        public static Context CreateInstance(this Context ctx, string dataCenterId, long userId)
        {
            Context contextByDataCenterId = DataCenterService.GetDataCenterContextByID(dataCenterId);

            //处理用户登录名
            {
                FormMetadata metadata = FormMetaDataCache.GetCachedFormMetaData(contextByDataCenterId, FormIdConst.SEC_User);
                BusinessInfo businessInfo = metadata.BusinessInfo.GetSubBusinessInfo(new List<string> { "FNumber", "FUserAccount", "FName" });
                DynamicObject dataObject = BusinessDataServiceHelper.LoadSingle(contextByDataCenterId, userId, businessInfo.GetDynamicObjectType());
                contextByDataCenterId.UserId = dataObject.PkId<long>();
                if (businessInfo.GetField("FNumber") != null) contextByDataCenterId.LoginName = dataObject.FieldProperty<string>(businessInfo.GetField("FNumber"));
                if (businessInfo.GetField("FUserAccount") != null) contextByDataCenterId.LoginName = dataObject.FieldProperty<string>(businessInfo.GetField("FUserAccount"));
                contextByDataCenterId.UserName = dataObject.FieldProperty<string>(businessInfo.GetField("FName"));
            }

            if (ctx == null) ctx = contextByDataCenterId;
            return contextByDataCenterId;
        }//end static method

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
