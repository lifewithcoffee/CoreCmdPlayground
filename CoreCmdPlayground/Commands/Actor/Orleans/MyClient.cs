using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansLib;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CoreCmdPlayground.Commands.Actor.Orleans
{
    public class MyClient
    {
        public async Task<int> RunAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MyClient";
                })
                .ConfigureLogging(logging => {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Warning);
                })
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host");
            return client;
        }

        private async Task DoClientWork(IClusterClient client)
        {
            // example of calling grains from the initialized client
            var friend = client.GetGrain<IHello>(0);
            var response = await friend.SayHello("Good morning, HelloGrain!");
            Console.WriteLine($"{response}");
        }
    }
}
