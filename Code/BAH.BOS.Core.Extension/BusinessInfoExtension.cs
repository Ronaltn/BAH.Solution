using Kingdee.BOS.Core.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.Metadata
{
    public static class BusinessInfoExtension
    {
        public static BusinessInfo GetSubBusinessInfo(this BusinessInfo businessInfo, params string[] fieldKeys)
        {
            return businessInfo.GetSubBusinessInfo(fieldKeys.ToList());
        }//end static method

    }//end static class
}//end namespace
