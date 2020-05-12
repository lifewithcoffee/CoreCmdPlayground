using CoreCmdPlaygroundLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using UtilityLib;
using static CoreCmdPlaygroundLib.StaticClass1;

//using static Microsoft.FSharp.Core.ModuleDemo;

namespace CoreCmdPlayground.Commands
{
    class DemoCommand
    {
        public void CallFsDll()
        {
            Print.Separator_______________________________________();
            Console.WriteLine("CallFsDll() is called, count = 5");

            new FsModuleDemo.FsYellCommand().Hey("Bob");
            FsNamespaceDemo.FsSayCommand.hey("Ben");
            new FsNamespaceDemo.FsSpeakCommand().Hey("Bok (from pip command)");
        }

        public void CallCsDll()
        {
            Print.Separator_______________________________________();

            Console.WriteLine("CallCsDll() is called, cout = 1");
            new Class1().Foo();
            StaticClass1.StaticFoo();
            StaticClass1.InnerStaticClass1.InnerStaticFoo();
            new InnerClass1().InnerFoo();
        }
    }
}