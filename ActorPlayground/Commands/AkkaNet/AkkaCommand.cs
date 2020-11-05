using Akka.Actor;
using CoreCmd.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ActorPlayground.Commands.AkkaNet
{
    class AkkaCommand
    {
        public void TestLocalActor()
        {
            Console.WriteLine("executing test1(): ");

            // initialize an actor system, i.e. a runtime or container of actors
            var system = ActorSystem.Create("MySystem");

            // create an actor and get its reference
            var greeter = system.ActorOf<GreetingActor>("greeter");

            for (int i = 0; i < 1000; i++)
            {
                // send message to the target actor
                greeter.Tell(new Greet($"greeting {i}"));
            }

            Thread.Sleep(1200);
            //Console.ReadKey();
        }
    }
}
