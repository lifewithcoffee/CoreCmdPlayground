using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoreCmdPlayground.Commands
{
    public class Greet
    {
        public Greet(string who)
        {
            Who = who;
        }
        public string Who { get; private set; }
    }

    public class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            Receive<Greet>(greet =>
               Console.WriteLine("Hello {0}", greet.Who));
        }
    }

    public class EchoActor : ReceiveActor
    {
        public EchoActor()
        {
            Receive<Hello>(hello =>
            {
                Console.WriteLine("[{0}]: {1}", Sender, hello.Message);
                Sender.Tell(hello);
            });
        }
    }

    public class Hello
    {
        public Hello(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }

    class SayHello { }

    class HelloActor : ReceiveActor
    {
        private IActorRef _remoteActor;
        private int _helloCounter;
        private ICancelable _helloTask;

        public HelloActor(IActorRef remoteActor)
        {
            _remoteActor = remoteActor;
            Receive<Hello>(hello =>
            {
                Console.WriteLine("Received {1} from {0}", Sender, hello.Message);
            });

            Receive<SayHello>(sayHello =>
            {
                _remoteActor.Tell(new Hello("hello" + _helloCounter++));
            });
        }

        protected override void PreStart()
        {
            Console.Write("HelloActor.PreStart() called.");
            _helloTask = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                  TimeSpan.FromSeconds(1)
                , TimeSpan.FromSeconds(1)
                , Context.Self
                , new SayHello()
                , ActorRefs.NoSender
                );
        }

        protected override void PostStop()
        {
            Console.Write("HelloActor.PostStop() called.");
            _helloTask.Cancel();
        }
    }

    class AkkaCommand
    {
        public void TestLocalActor()
        {
            Console.WriteLine("executing test1(): ");

            // initialize an actor system, i.e. a runtime or container of actors
            var system = ActorSystem.Create("MySystem");

            // create an actor and get its reference
            var greeter = system.ActorOf<GreetingActor>("greeter");

            for(int i = 0; i < 1000; i++)
            {
                // send message to the target actor
                greeter.Tell(new Greet($"greeting {i}"));
            }

            Thread.Sleep(1200);
            //Console.ReadKey();
        }

        public void StartDeployTarget()
        {
            try
            {
                using (var system = ActorSystem.Create("DeployTarget", ConfigurationFactory.ParseString(@"akka {
                        actor.provider = remote
                        remote {
                            dot-netty.tcp {
                                port = 8090
                                hostname = localhost
                            }
                        }
                    }")))
                {
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void StartDeployer()
        {
            using (var system = ActorSystem.Create("Deployer", ConfigurationFactory.ParseString(@"
            akka {  
                actor{
                    provider = remote
                    deployment {
                        /remoteecho {
                            remote = ""akka.tcp://DeployTarget@localhost:8090""
                        }
                    }
                }
                remote {
                    dot-netty.tcp {
                        port = 0
                        hostname = localhost
                    }
                }
            }")))
            {
                var remoteAddress = Address.Parse("akka.tcp://DeployTarget@localhost:8090");

                //deploy remotely via config
                var remoteEcho1 = system.ActorOf(Props.Create(() => new EchoActor()), "remoteecho");

                //deploy remotely via code
                var remoteEcho2 =
                    system.ActorOf(
                        Props.Create(() => new EchoActor())
                            .WithDeploy(Deploy.None.WithScope(new RemoteScope(remoteAddress))), "coderemoteecho");


                // create local actors
                system.ActorOf(Props.Create(() => new HelloActor(remoteEcho1)));
                system.ActorOf(Props.Create(() => new HelloActor(remoteEcho2)));

                Console.ReadKey();
            }
        }
    }
}
