using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.Metadata.ConvertElement
{
    public static class ConvertRuleMetaDataExtension
    {
        public static void ThrowWhenNull(this ConvertRuleElement element)
        {
            if (element == null) throw new KDBusinessException(string.Empty, "未配置有效的单据转换规则！");
        }

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
