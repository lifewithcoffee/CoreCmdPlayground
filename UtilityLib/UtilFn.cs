using System;
using System.Diagnostics;

namespace UtilityLib
{
    public static class Print
    {
        public static void Separator_______________________________________()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);        // get caller method's frame info

            var currentMethodName = sf.GetMethod();
            Console.WriteLine($"----------------------------------------------{currentMethodName.Name}");
        }

    }
}
