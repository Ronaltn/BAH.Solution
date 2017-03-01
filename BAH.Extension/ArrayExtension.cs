using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ArrayExtension
    {
        public static void ForEach<T>(this T[] source, Action<T> action)
        {
            foreach (var obj in source)
            {
                action(obj);
            }
        }

    }//end class
}//end namespace
