using Kingdee.BOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS
{
    public static class LocaleValueExtension
    {
        public static string Value(this LocaleValue localeValue, int localeId)
        {
            return localeValue == null ? string.Empty : localeValue.GetString(localeId);
        }

        public static string Value(this LocaleValue localeValue, Context ctx)
        {
            return Value(localeValue, ctx.UserLocale.LCID);
        }
    }//end class
}//end namespace
