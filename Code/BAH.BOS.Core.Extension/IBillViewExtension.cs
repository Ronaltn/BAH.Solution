using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Core.Metadata.FormElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.Bill
{
    /// <url>
    /// http://club.kingdee.com/forum.php?mod=viewthread&tid=945654&extra=&page=1#page
    /// </url>
    public static class IBillViewExtension
    {
        public static IBillView AddNew(this IBillView billView)
        {
            //新建一个空白数据
            billView.CreateNewModelData();
            ((IBillViewService)billView).LoadData();

            // 触发插件的OnLoad事件：
            // 组织控制基类插件，在OnLoad事件中，对主业务组织改变是否提示选项进行初始化。
            // 如果不触发OnLoad事件，会导致主业务组织赋值不成功
            DynamicFormViewPlugInProxy eventProxy = billView.GetService<DynamicFormViewPlugInProxy>();
            eventProxy.FireOnLoad();

            //设置FormId
            Form form = billView.BillBusinessInfo.GetForm();
            if (form.FormIdDynamicProperty != null)
            {
                form.FormIdDynamicProperty.SetValue(billView.Model.DataObject, form.Id);
            }//end if

            return billView;
        }

        public static IBillView Edit(this IBillView billView, object id)
        {
            billView.OpenParameter.Status = OperationStatus.EDIT;
            billView.OpenParameter.CreateFrom = CreateFrom.Default;
            billView.OpenParameter.PkValue = id;
            billView.OpenParameter.DefaultBillTypeId = string.Empty;
            ((IDynamicFormViewService)billView).LoadData();

            //设置FormId
            Form form = billView.BillBusinessInfo.GetForm();
            if (form.FormIdDynamicProperty != null)
            {
                form.FormIdDynamicProperty.SetValue(billView.Model.DataObject, form.Id);
            }//end if

            return billView;
        }

    }//end static class
}//end namespace
