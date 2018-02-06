using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.DynamicForm.PlugIn.Args
{
    public static class EndOperationTransactionArgsExtension
    {
        public static EndSetStatusTransactionArgs AsEndSetStatusTransactionArgs(this EndOperationTransactionArgs args)
        {
            return args.AsType<EndSetStatusTransactionArgs>();
        }
    }
}
