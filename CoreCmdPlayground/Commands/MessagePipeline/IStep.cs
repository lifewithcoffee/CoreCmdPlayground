namespace CoreCmdPlayground.Commands.MessagePipeline
{
    public interface IStep<T>
    {
        bool Execute(T msg);
        IStep<T> AddNextStep(IStep<T> next);
    }

    public interface IStep2<T>
    {
        bool Execute(T msg);
    }    
}