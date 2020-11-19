using CoreCmd.CommandExecution;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.OffScreen
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new AssemblyCommandExecutor().ExecuteAsync(args, services => { });
        }
    }
}