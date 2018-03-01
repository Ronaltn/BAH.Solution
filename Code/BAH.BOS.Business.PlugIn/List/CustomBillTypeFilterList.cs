using Kingdee.BOS.Core.Const;
using Kingdee.BOS.Core.List.PlugIn;
using Kingdee.BOS.Core.List.PlugIn.Args;
using Kingdee.BOS.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BAH.BOS.Business.PlugIn.List
{
    [Description("根据单据类型参数过滤，列表插件。")]
    public class CustomBillTypeFilterList : AbstractListPlugIn
    {
        public override void PrepareFilterParameter(FilterArgs e)
        {
            base.PrepareFilterParameter(e);
            this.ListView.OpenParameter.GetCustomParameter(BOSConst.CustomBillTypeID)
                .Adaptive(obj => obj.ToChangeTypeOrDefault<string>())
                .Adaptive(val => val.IsNullOrEmptyOrWhiteSpace().False(() =>
                {
                    var field = this.ListModel.BillBusinessInfo.GetBillTypeField();
                    if (field == null) return;
                    e.AppendQueryFilter(string.Format("{0} = '{1}'", field.Key, val));
                }));
        }//end method

    }//end class
}//end namespace
