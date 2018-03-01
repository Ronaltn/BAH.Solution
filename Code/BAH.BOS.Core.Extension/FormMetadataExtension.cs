using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.Bill;
using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Core.Metadata.FormElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.Metadata
{
    /// <url>
    /// http://club.kingdee.com/forum.php?mod=viewthread&tid=945654&extra=&page=1#page
    /// </url>
    public static class FormMetadataExtension
    {
        public static IBillView CreateBillView(this FormMetadata metadata, Context ctx, Action<BillOpenParameter> openParamAction = null)
        {
            //创建用于引入数据的单据view
            Type type = Type.GetType("Kingdee.BOS.Web.Import.ImportBillView,Kingdee.BOS.Web");
            var billView = (IDynamicFormViewService)Activator.CreateInstance(type);

            //获取表单元素
            Form form = metadata.BusinessInfo.GetForm();

            //开始初始化billView：
            //创建视图加载参数对象，指定各种参数，如FormId, 视图(LayoutId)等
            //指定FormId, LayoutId
            BillOpenParameter openParam = new BillOpenParameter(form.Id, metadata.GetLayoutInfo().Id);
            openParam.Context = ctx;//数据库上下文
            openParam.ServiceName = form.FormServiceName;//本单据模型使用的MVC框架
            openParam.PageId = Guid.NewGuid().ToString();//随机产生一个不重复的PageId，作为视图的标识
            openParam.FormMetaData = metadata;//元数据
            openParam.Status = OperationStatus.ADDNEW;//界面状态：新增 (修改、查看)
            openParam.PkValue = null;//单据主键：本案例演示新建物料，不需要设置主键
            openParam.CreateFrom = CreateFrom.Default;//界面创建目的：普通无特殊目的 （为工作流、为下推、为复制等）
            openParam.GroupId = string.Empty;//基础资料分组维度：基础资料允许添加多个分组字段，每个分组字段会有一个分组维度。具体分组维度Id，请参阅 form.FormGroups 属性
            openParam.ParentId = 0;//基础资料分组：如果需要为新建的基础资料指定所在分组，请设置此属性
            openParam.DefaultBillTypeId = string.Empty; //单据类型
            openParam.DefaultBusinessFlowId = string.Empty;//业务流程
            openParam.SetCustomParameter("ShowConfirmDialogWhenChangeOrg", false);//主业务组织改变时，不用弹出提示界面
            openParamAction?.Invoke(openParam);

            //插件
            List<AbstractDynamicFormPlugIn> plugs = form.CreateFormPlugIns();
            openParam.SetCustomParameter(FormConst.PlugIns, plugs);
            PreOpenFormEventArgs args = new PreOpenFormEventArgs(ctx, openParam);
            foreach (var plug in plugs)
            {
                //触发插件PreOpenForm事件，供插件确认是否允许打开界面
                plug.PreOpenForm(args);
            }
            if (args.Cancel == true)
            {
                //插件不允许打开界面
                //本案例不理会插件的诉求，继续....
            }//end if

            //动态领域模型服务提供类，通过此类，构建MVC实例
            var provider = form.GetFormServiceProvider();
            billView.Initialize(openParam, provider);
            return billView as IBillView;
        }

    }//end static class
}//end namespace
