using CoreCmdPlaygroundLib;
using NamespaceDemo;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreCmdPlaygroundLib.StaticClass1;
using static ModuleDemo;

//using static Microsoft.FSharp.Core.ModuleDemo;

namespace CoreCmdPlayground.Commands
{
    class DemoCommand
    {
        public void CallFsdll()
        {
            Console.WriteLine("PipeCommand.Test() is called");
            //new YellCommand().Hey("Bob");
            //SayCommand.hey("Ben");
            new SpeakCommand().Hey("Bok (from pip command)");
        }

        public void CallCsdll()
        {
            Console.WriteLine("PipeCommand.Test2() is called");
            new Class1().Foo();
            StaticClass1.StaticFoo();
            StaticClass1.InnerStaticClass1.InnerStaticFoo();
            new InnerClass1().InnerFoo();
        }
    }
}