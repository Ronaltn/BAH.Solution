using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class DecimalExtension
    {
        public static string ToTrimEndZeroString(this decimal d)
        {
            return d.ToString().TrimEnd(new char[] { '0', '.' });
        }
    }
}
