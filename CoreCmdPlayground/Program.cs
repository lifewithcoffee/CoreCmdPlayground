using System;
using System.Threading.Tasks;
using CoreCmd.CommandExecution;
using CoreCmdPlayground.Commands.MediatR;
using HttpClientLib;

namespace CoreCmdPlayground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new AssemblyCommandExecutor().ExecuteAsync(args, services => {
                services.AddHttpServices();
                services.AddMediatRDemo();

                /**
                 * For unknown reason, logging to console can't work
                 */
                //services.AddLogging(builder =>
                //{
                //    builder.AddConsole();
                //    builder.SetMinimumLevel(LogLevel.Debug);
                //});
            });
        }
    }
}
