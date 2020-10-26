using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoreCmdPlayground.Commands.Actor.AkkaNet
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
