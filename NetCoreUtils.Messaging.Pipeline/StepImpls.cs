using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreUtils.Messaging.Pipeline
{
    public class Message1
    {
        public string Data { get; set; }
    }

    public class Step1 : AbstractStep<Message1>
    {
        public override bool Handle(Message1 msg)
        {
            msg.Data = "data1";
            Console.WriteLine($"Do step1 data = {msg.Data}");
            return true;
        }
    }

    public class Step2 : AbstractStep<Message1>
    {
        public override bool Handle(Message1 msg)
        {
            msg.Data = "data2";
            Console.WriteLine($"Do step2 data = {msg.Data}");
            return true;
        }
    }

    public class Step3 : AbstractStep<Message1>
    {
        public override bool Handle(Message1 msg)
        {
            msg.Data = "data3";
            Console.WriteLine($"Do step3 data = {msg.Data}");
            return true;
        }
    }

    public class Step4 : AbstractStep<Message1>
    {
        public override bool Handle(Message1 msg)
        {
            msg.Data = "data4";
            Console.WriteLine($"Do step4 data = {msg.Data}");
            return true;
        }
    }
}