using System;
using System.Threading.Tasks;
using Orleans;

namespace OrleansLib
{
    public interface IHello : IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }

    public class HelloGrain : Grain, IHello
    {
        Task<string> IHello.SayHello(string greeting)
        {
            Console.WriteLine($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"Client said: '{greeting}', so HelloGrain says: Hello!");
        }
    }
}
