using CoreCmd.CommandExecution;
using System;
using System.Threading.Tasks;

namespace CoreCmdPlayground.Scaffold
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new AssemblyCommandExecutor().ExecuteAsync(args);
        }
    }
}
