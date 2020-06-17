using System;
using System.Threading.Tasks;
using CoreCmd.CommandExecution;

namespace CoreCmdPlayground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new AssemblyCommandExecutor().ExecuteAsync(args);
        }
    }
}
