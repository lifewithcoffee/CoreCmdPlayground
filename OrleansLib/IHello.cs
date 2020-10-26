using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace OrleansLib
{
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }

    public class HelloGrain : Orleans.Grain, IHello
    {
        Task<string> IHello.SayHello(string greeting)
        {
            Console.WriteLine($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"Client said: '{greeting}', so HelloGrain says: Hello!");
        }
    }
}
