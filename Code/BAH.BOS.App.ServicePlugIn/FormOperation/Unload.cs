using BAH.BOS.Core.Const.BillStatus;
using BAH.BOS.Core.Const.FormOperation;
using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Core.Interaction;
using Kingdee.BOS.Core.Metadata.FieldElement;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.Util;
using System;
using System.ComponentModel;
using System.Linq;

namespace BAH.BOS.App.ServicePlugIn.FormOperation
{
    [Description("表单操作插件，回撤操作。")]
    public class Unload : AbstractOperationServicePlugIn
    {
        public override void OnPrepareOperationServiceOption(OnPrepareOperationServiceEventArgs e)
        {
            base.OnPrepareOperationServiceOption(e);
            if (this.Option == null)
            {
                this.Option.SetIgnoreWarning(true);
                this.Option.SetIgnoreInteractionFlag(true);
            }//end if
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            base.OnPreparePropertys(e);
            e.FieldKeys.Add(this.BusinessInfo.GetForm().DocumentStatusFieldKey);
        }

        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {
            base.BeginOperationTransaction(e);

            var documentStatusField = this.BusinessInfo.GetField(this.BusinessInfo.GetForm().DocumentStatusFieldKey).AsType<BillStatusField>();

            //如果数据状态是审核中，需先执行撤销操作。
            if (!e.CancelFormService && !e.CancelOperation)
            {
                var dataEntities = e.DataEntitys
                                    .Where(data => data.FieldProperty<string>(documentStatusField).EqualsIgnoreCase(DocumentStatus.Instance.Approving()))
                                    .ToArray();
                if (dataEntities.Any())
                {
                    dataEntities.CancelAssign(this.Context, this.BusinessInfo, this.Option).Adaptive(result =>
                    {
                        if (!result.IsSuccess)
                        {
                            e.CancelFormService = true;
                            e.CancelOperation = true;
                            this.OperationResult.MergeResult(result);
                            if (this.Option.GetThrowWhenUnSuccess()) this.OperationResult.ThrowWhenUnSuccess(false);
                        }//end if
                    });
                }//end if

            }//end if

            //如果数据状态是已审核，需先执行反审核操作。
            if (!e.CancelFormService && !e.CancelOperation)
            {
                var dataEntities = e.DataEntitys
                                    .Where(data => data.FieldProperty<string>(documentStatusField).EqualsIgnoreCase(DocumentStatus.Instance.Approved()))
                                    .ToArray();
                if (dataEntities.Any())
                {
                    dataEntities.UnAudit(this.Context, this.BusinessInfo, this.Option).Adaptive(result =>
                    {
                        if (!result.IsSuccess)
                        {
                            e.CancelFormService = true;
                            e.CancelOperation = true;
                            this.OperationResult.MergeResult(result);
                            if (this.Option.GetThrowWhenUnSuccess()) this.OperationResult.ThrowWhenUnSuccess(false);
                        }//end if
                    });
                }//end if

            }//end if

            //删除
            if (!e.CancelFormService && !e.CancelOperation)
            {
                e.DataEntitys.Delete(this.Context, this.BusinessInfo, this.Option).Adaptive(result =>
                {
                    if (!result.IsSuccess)
                    {
                        e.CancelFormService = true;
                        e.CancelOperation = true;
                        this.OperationResult.MergeResult(result);
                        if (this.Option.GetThrowWhenUnSuccess()) this.OperationResult.ThrowWhenUnSuccess(false);
                    }//end if
                });
            }//end if

        }//end method

    }//end class
}//end namespace
