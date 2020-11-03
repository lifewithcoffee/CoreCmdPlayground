using System;
using System.Collections.Generic;

namespace CoreCmdPlayground.Commands.MessagePipeline
{
    class Procedure<T>
    {
        IStep<T> firstStep;
        IStep<T> lastStep;

        public Procedure<T> AddStep(IStep<T> next)
        {
            if (firstStep == null)
                firstStep = next;
            lastStep?.AddNextStep(next);
            lastStep = next;
            return this;
        }

        public Procedure<T> AddStep(Func<T, bool> fn)
        {
            return this.AddStep(new GeneralStep<T>(fn));
        }

        public void Execute(T msg)
        {
            firstStep?.Execute(msg);
        }
    }

    class Procedure2<T>
    {
        List<IStep2<T>> steps = new List<IStep2<T>>();

        public Procedure2<T> AddStep(IStep2<T> step)
        {
            steps.Add(step);
            return this;
        }

        public Procedure2<T> AddStep(Func<T, bool> fn)
        {
            steps.Add(new GeneralStep2<T>(fn));
            return this;
        }

        public void Execute(T msg)
        {
            foreach(var step in steps)
            {
                if (!step.Execute(msg))
                    break;
            }
        }
    }
}