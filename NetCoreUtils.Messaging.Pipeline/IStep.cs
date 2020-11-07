namespace NetCoreUtils.Messaging.Pipeline
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

    //public interface IStep<TIn, TOut>
    //{
    //    bool Execute(TIn msg);
    //    IStep
    //}
}