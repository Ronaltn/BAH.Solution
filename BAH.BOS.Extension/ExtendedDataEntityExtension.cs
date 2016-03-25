using Kingdee.BOS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core
{
    public static class ExtendedDataEntityExtension
    {
        public static T Property<T>(this ExtendedDataEntity dataObject, string propertyName)
        {
            if (dataObject[propertyName] == null)
            {
                return default(T);
            }
            else
            {
                return dataObject[propertyName].ToType<T>();
            }
        }//end method
    }//end class
}//end namespace
