using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class BoolExtension
    {
        public static bool True(this bool b, Action action)
        {
            if (b) action?.Invoke();
            return b;
        }

        public static bool False(this bool b, Action action)
        {
            if (!b) action?.Invoke();
            return b;
        }

    }//end static class
}//end namespace
