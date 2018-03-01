using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.Json
{
    public class ShortDateConverter : IsoDateTimeConverter
    {
        public ShortDateConverter()
        {
            this.DateTimeFormat = "yyyy'-'MM'-'dd";
        }
    }
}
