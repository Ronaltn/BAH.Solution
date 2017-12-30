using Kingdee.BOS.Core.Metadata.ConvertElement.PlugIn;
using Kingdee.BOS.Core.Metadata.ConvertElement.PlugIn.Args;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BAH.BOS.App.ServicePlugIn.ConvertRule
{
    [Description("单据转换插件，忽略创建关联数据。")]
    public class IgnoreCreateLink : AbstractConvertPlugIn
    {
        public override void OnCreateLink(CreateLinkEventArgs e)
        {
            //不创建Link表数据。
            e.Cancel = true;
        }
    }
}
