using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.DynamicForm.PlugIn.Args
{
    public static class BeginSetStatusTransactionArgsExtension
    {
        public static BeginSetStatusTransactionArgs AsBeginSetStatusTransactionArgs(this BeginOperationTransactionArgs args)
        {
            return args.AsType<BeginSetStatusTransactionArgs>();
        }
    }
}
