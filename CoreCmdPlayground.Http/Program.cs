using System;
using System.Threading.Tasks;
using CoreCmd.CommandExecution;
using HttpClientLib;

namespace CoreCmdPlayground.Http
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new AssemblyCommandExecutor().ExecuteAsync(args, services => {
                services.AddHttpServices();
            });
        }
    }
}
