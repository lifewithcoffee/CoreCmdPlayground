using CoreCmd.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace CoreCmdPlayground.Commands
{
    [Alias("proc")]
    class ProcessCommand
    {
        public void Test1()
        {
            Console.WriteLine("ProcessCommand.Test1() called");
            var proc = Process.Start("dotnet", @"E:\rp\git\CoreCmdPlayground\CoreCmdPlayground\bin\Debug\netcoreapp3.1\corecmdplayground.dll proc test2");
            Thread.Sleep(5000);
            //Process.GetCurrentProcess().Kill();
            //proc.WaitForExit();
            Console.WriteLine("ProcessCommand.Test1() ended");
        }


        public void Test2()
        {
            Console.WriteLine("ProcessCommand.Test2() called");
            Thread.Sleep(15000);
            Console.WriteLine("ProcessCommand.Test2() ended");
        }
    }
}
