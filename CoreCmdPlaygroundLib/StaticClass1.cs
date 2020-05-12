using System;
using System.Collections.Generic;
using System.Text;
using UtilityLib;

namespace CoreCmdPlaygroundLib
{
    public static class StaticClass1
    {
        public static void StaticFoo()
        {
            Print.Separator_______________________________________();
            Console.WriteLine("StaticClass1.StaticFoo() called");
        }
        
        public class InnerStaticClass1
        {
            public static void InnerStaticFoo()
            {
                Print.Separator_______________________________________();
                Console.WriteLine("InnerStaticClass1.InnerStaticFoo() called");
            }
        }

        public class InnerClass1
        {
            public void InnerFoo()
            {
                Print.Separator_______________________________________();
                Console.WriteLine("InnerClass1.InnerFoo() called");
            }
        }
    }
}
