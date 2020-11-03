using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCmdPlayground.Commands.MessagePipeline
{
    public class Message1
    {
        public string Data { get; set; }
    }

    class PipelineCommand
    {
        public void Do()
        {
            new Procedure<Message1>()
                .AddStep(new Step1())
                .AddStep(new Step2())
                .AddStep(new Step3())
                .AddStep(new Step4())
                .AddStep(msg => {
                    Console.WriteLine($"Do Fun<,> step, data = {msg.Data}");
                    return true;
                })
                .Execute(new Message1());
        }

        class step2a : IStep2<Message1>
        {
            public bool Execute(Message1 msg)
            {
                Console.WriteLine(msg.Data);
                msg.Data = "step2a";
                return true;
            }
        }

        class step2b : IStep2<Message1>
        {
            public bool Execute(Message1 msg)
            {
                Console.WriteLine(msg.Data);
                msg.Data = "step2b";
                return false;
            }
        }

        public void Do2()
        {
            new Procedure2<Message1>()
                .AddStep(msg =>
                {
                    Console.WriteLine(msg.Data);
                    msg.Data = "111";
                    return true;
                })
                .AddStep(msg =>
                {
                    Console.WriteLine(msg.Data);
                    msg.Data = "222";
                    return true;
                })
                .AddStep(new step2a())
                .AddStep(new step2b())
                .AddStep(msg =>
                {
                    Console.WriteLine(msg.Data);
                    msg.Data = "333";
                    return true;
                })
                .Execute(new Message1 { Data = "init" });
        }
    }
}
