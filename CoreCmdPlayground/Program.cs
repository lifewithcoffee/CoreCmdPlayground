using System;
using CoreCmd;
using CoreCmd.CommandExecution;

namespace CoreCmdPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            new AssemblyCommandExecutor().Execute(args);
        }
    }
}
