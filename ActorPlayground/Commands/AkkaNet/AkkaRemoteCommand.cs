using Akka.Actor;
using Akka.Configuration;
using System;

namespace CoreCmdPlayground.Commands.Actor.AkkaNet
{
    class AkkaRemoteCommand
    { 
        public void DeployRemote()
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
