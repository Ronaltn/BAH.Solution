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
            var i = (int)d;
            if(d == i)
            {
                return i.ToString();
            }
            else
            {
                var tail = d.ToString().Adaptive(s => s.Substring(s.IndexOf('.') + 1)).TrimEnd('0');
                return string.Concat(i.ToString(), ".", tail);
            }
        }
    }
}
