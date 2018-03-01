using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.Json
{
    public class LongDateConverter : IsoDateTimeConverter
    {
        public LongDateConverter()
        {
            this.DateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss";
        }
    }
}
