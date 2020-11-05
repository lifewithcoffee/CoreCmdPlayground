using System;
using System.Collections.Generic;
using System.Text;

namespace ActorPlayground.Commands.AkkaNet
{
    public class Greet
    {
        public Greet(string who)
        {
            Who = who;
        }
        public string Who { get; private set; }
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

}
