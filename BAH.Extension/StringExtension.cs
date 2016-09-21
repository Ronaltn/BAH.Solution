using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class StringExtension
    {
        public static string EmptyWhenNull(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? string.Empty : str;
        }
    }
}
