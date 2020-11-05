using Akka.Actor;
using System;

namespace CoreCmdPlayground.Commands.Actor.AkkaNet
{
    public class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            Receive<Greet>(greet =>
               Console.WriteLine("Hello {0}", greet.Who));
        }
    }
}
