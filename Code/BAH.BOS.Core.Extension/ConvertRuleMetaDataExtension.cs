using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.Metadata.ConvertElement
{
    public static class ConvertRuleMetaDataExtension
    {
        public static T GetConvertPolicyElement<T>(this ConvertRuleElement element) where T : ConvertPolicyElement
        {
            return element.Policies.Where(police => police is T)
                                   .Cast<T>()
                                   .FirstOrDefault();
        }

        public static DefaultConvertPolicyElement GetDefaultConvertPolicyElement(this ConvertRuleElement element)
        {
            return GetConvertPolicyElement<DefaultConvertPolicyElement>(element);
        }
    }
}
