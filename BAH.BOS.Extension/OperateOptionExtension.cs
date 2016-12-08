using Kingdee.BOS.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.BOS.Util;
using Kingdee.BOS.Core.Interaction;

namespace Kingdee.BOS.Orm
{
    public static class OperateOptionExtension
    {
        public static OperateOption SetIgnoreOption(this OperateOption option)
        {
            option.SetIgnoreWarning(true);
            option.SetIgnoreInteractionFlag(true);
            //option.SetIgnoreScopeValidateFlag(true);
            return option;
        }//end static class

    }//end class
}//end namespace
