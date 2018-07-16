using CoreCmdPlayground.Services;
using NamespaceDemo;
using System;
using System.Collections.Generic;
using System.Text;
using static ModuleDemo;

namespace CoreCmdPlayground.Commands
{
    class PipeCommand
    {
        public void Test()
        {
            Console.WriteLine("PipeCommand.Test() is called");
            new YellCommand().Hey("Bob");
            SayCommand.hey("Ben");
            new SpeakCommand().Hey("Bok (from pip command)");
        }
    }
}