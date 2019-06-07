using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCmdPlaygroundLib
{
    public static class StaticClass1
    {
        public static void StaticFoo()
        {
            Console.WriteLine("StaticClass1.StaticFoo() called");
        }
        
        public class InnerStaticClass1
        {
            public static void InnerStaticFoo()
            {
                Console.WriteLine("InnerStaticClass1.InnerStaticFoo() called");
            }
        }

        public class InnerClass1
        {
            public void InnerFoo()
            {
                Console.WriteLine("InnerClass1.InnerFoo() called");
            }
        }
    }
}
