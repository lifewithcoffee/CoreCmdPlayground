using System;
using System.Threading.Tasks;
using CoreCmd.CommandExecution;
using HttpClientLib;
using InfluxDBTestLib;

namespace CoreCmdPlayground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new AssemblyCommandExecutor().ExecuteAsync(args, services => {
                services.AddHttpServices();
                services.AddInfluxDb( new InfluxDbSetting { 
                    Token = "4R1aL7t1hZolnMQezXQxkhhMGlqYUBy7g5Ue8RQAQ9wHn_XIHJN_2EpFqaYcD9F2wv_lt-kHqP8Ym99c7Gv5pw==" 
                });
            });
        }
    }
}
