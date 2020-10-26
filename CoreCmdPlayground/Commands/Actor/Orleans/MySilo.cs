using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansLib;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CoreCmdPlayground.Commands.Actor.Orleans
{
    public class MySilo
    {
        public async Task<int> RunAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();
                // await host.StopAsync(); // can't quit after press enter if enable this line
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MySilo";
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => { 
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Warning);
                });

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
