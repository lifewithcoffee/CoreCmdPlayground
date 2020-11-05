using Akka.Actor;
using System;

namespace ActorPlayground.Commands.AkkaNet
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
