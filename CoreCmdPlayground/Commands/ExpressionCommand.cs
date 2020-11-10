using CoreCmd.Attributes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CoreCmdPlayground.Commands
{
    static class TestClass
    {
        static public void TestMethod1()
        {
            Console.WriteLine("TestMethod1() called");
        }

        static public void TestMethod2()
        { 
            Console.WriteLine("TestMethod2() called");
        }
    }

    [Alias("expr")]
    class ExpressionCommand
    {
        public void CallStaticMethod()
        {
            try
            {
                var expr = Expression.Call(typeof(TestClass).GetMethod(nameof(TestClass.TestMethod1)));
                Action fn = Expression.Lambda<Action>(expr).Compile();
                fn();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
