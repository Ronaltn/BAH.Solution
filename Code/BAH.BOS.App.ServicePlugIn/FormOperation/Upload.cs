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
    [Description("表单操作插件，上传操作。")]
    public class Upload : AbstractOperationServicePlugIn
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

        public override void BeforeExecuteOperationTransaction(BeforeExecuteOperationTransaction e)
        {
            base.BeforeExecuteOperationTransaction(e);
            var documentStatusField = this.BusinessInfo.GetField(this.BusinessInfo.GetForm().DocumentStatusFieldKey).AsType<BillStatusField>();

            //如果数据状态是审核中，需先执行撤销操作。
            {
                var dataEntities = e.SelectedRows
                                    .Select(row => row.DataEntity)
                                    .Where(data => data.FieldProperty<string>(documentStatusField).EqualsIgnoreCase(DocumentStatus.Instance.Approving()))
                                    .ToArray();
                if (dataEntities.Any())
                {
                    var result = dataEntities.CancelAssign(this.Context, this.BusinessInfo, this.Option);
                    if (!result.IsSuccess)
                    {
                        e.Cancel = true;
                        e.CancelMessage = string.Concat(e.CancelMessage, result.GetResultMessage());
                        this.OperationResult.MergeResult(result);
                    }//end if
                }//end if
            }

            //如果数据状态是已审核，需先执行反审核操作。
            {
                var dataEntities = e.SelectedRows
                                    .Select(row => row.DataEntity)
                                    .Where(data => data.FieldProperty<string>(documentStatusField).EqualsIgnoreCase(DocumentStatus.Instance.Approved()))
                                    .ToArray();
                if (dataEntities.Any())
                {
                    var result = dataEntities.UnAudit(this.Context, this.BusinessInfo, this.Option);
                    if (!result.IsSuccess)
                    {
                        e.Cancel = true;
                        e.CancelMessage = string.Concat(e.CancelMessage, result.GetResultMessage());
                        this.OperationResult.MergeResult(result);
                    }//end if
                }//end if
            }

            //如果无需中断，则执行暂存+保存操作。
            if (!e.Cancel)
            {
                var dataEntities = e.SelectedRows
                                    .Select(row => row.DataEntity)
                                    .ToArray();

                //记下重新审核的数据
                var reCreatedDataEntities = dataEntities.Where(data => data.FieldProperty<string>(documentStatusField).EqualsIgnoreCase(DocumentStatus.Instance.ReCreated()))
                                                        .ToArray();

                //暂存
                if (this.Option.GetCutoffOperation().Adaptive(number => number.IsNullOrEmptyOrWhiteSpace() ||
                                                                        number.EqualsIgnoreCase(FormOperationEnum.Draft.ToString()) ||
                                                                        number.EqualsIgnoreCase(FormOperationEnum.Save.ToString()) ||
                                                                        number.EqualsIgnoreCase(FormOperationEnum.Submit.ToString()) ||
                                                                        number.EqualsIgnoreCase(FormOperationEnum.Audit.ToString())))
                {
                    if (!this.Option.GetOutOfTransaction()) dataEntities.ForEach(data => documentStatusField.DynamicProperty.SetValue(data, DocumentStatus.Instance.Draft()));
                    dataEntities.Draft(this.Context, this.BusinessInfo, this.Option).Adaptive(result =>
                    {
                        if (!result.IsSuccess) this.OperationResult.MergeResult(result);
                    });
                }//end if

                //保存
                if (this.Option.GetOutOfTransaction() &&
                    this.Option.GetCutoffOperation().Adaptive(number => number.IsNullOrEmptyOrWhiteSpace() ||
                                                                        number.EqualsIgnoreCase(FormOperationEnum.Save.ToString()) ||
                                                                        number.EqualsIgnoreCase(FormOperationEnum.Submit.ToString()) ||
                                                                        number.EqualsIgnoreCase(FormOperationEnum.Audit.ToString())))
                {
                    reCreatedDataEntities.ForEach(data => documentStatusField.DynamicProperty.SetValue(data, DocumentStatus.Instance.ReCreated()));
                    dataEntities.Save(this.Context, this.BusinessInfo, this.Option).Adaptive(result =>
                    {
                        if (!result.IsSuccess)
                        {
                            e.Cancel = true;
                            e.CancelMessage = string.Concat(e.CancelMessage, result.GetResultMessage());
                            this.OperationResult.MergeResult(result);
                        }//end if
                    });
                }//end if

            }//end if
        }//end method

        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {
            base.BeginOperationTransaction(e);

            //保存
            if (!e.CancelOperation &&
                !e.CancelOperation &&
                !this.Option.GetOutOfTransaction() && 
                this.Option.GetCutoffOperation().Adaptive(number => number.IsNullOrEmptyOrWhiteSpace() ||
                                                                    number.EqualsIgnoreCase(FormOperationEnum.Save.ToString()) ||
                                                                    number.EqualsIgnoreCase(FormOperationEnum.Submit.ToString()) ||
                                                                    number.EqualsIgnoreCase(FormOperationEnum.Audit.ToString())))
            {
                e.DataEntitys.Save(this.Context, this.BusinessInfo, this.Option).Adaptive(result =>
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

            //提交
            if (!e.CancelOperation &&
                !e.CancelOperation && 
                this.Option.GetCutoffOperation().Adaptive(number => number.IsNullOrEmptyOrWhiteSpace() ||
                                                                    number.EqualsIgnoreCase(FormOperationEnum.Submit.ToString()) ||
                                                                    number.EqualsIgnoreCase(FormOperationEnum.Audit.ToString())))
            {
                e.DataEntitys.Submit(this.Context, this.BusinessInfo, this.Option).Adaptive(result =>
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

            //审核
            if (!e.CancelOperation &&
                !e.CancelOperation && 
                this.Option.GetCutoffOperation().Adaptive(number => number.IsNullOrEmptyOrWhiteSpace() ||
                                                          number.EqualsIgnoreCase(FormOperationEnum.Audit.ToString())))
            {
                e.DataEntitys.Audit(this.Context, this.BusinessInfo, this.Option).Adaptive(result =>
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
