using System;
using CoreCmd;

namespace CoreCmdPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            new CommandExecutor().Execute(args);
        }
    }
}
