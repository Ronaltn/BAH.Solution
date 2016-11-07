using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class DateTimeExtension
    {
        public static DateTime FirstDayOfMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        public static DateTime LastDayOfMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }
    }
}
