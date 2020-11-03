namespace CoreCmdPlayground.Commands.MessagePipeline
{
    abstract public class AbstractStep<T> : IStep<T>
    {
        IStep<T> nextStep;

        public bool Execute(T msg)
        {
            if(Handle(msg))
            {
                if (nextStep != null)
                    return nextStep.Execute(msg);
            }

            return false;
        }

        abstract public bool Handle(T msg);

        public IStep<T> AddNextStep(IStep<T> next)
        {
            nextStep = next;
            return nextStep;
        }
    }
}