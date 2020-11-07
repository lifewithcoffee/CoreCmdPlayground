using System;

namespace NetCoreUtils.Messaging.Pipeline
{
    public class GeneralStep<T> : AbstractStep<T>
    {

        Func<T, bool> _fn = T => { return false; };

        public GeneralStep(Func<T,bool> fn)
        {
            _fn = fn;
        }

        public override bool Handle(T msg)
        {
            return _fn(msg);
        }
    }

    public class GeneralStep2<T> : IStep2<T>
    {
        Func<T, bool> _fn = T => { return false; };

        public GeneralStep2(Func<T, bool> fn)
        {
            _fn = fn;
        }

        public bool Execute(T msg)
        {
            return _fn(msg);
        }
    }
}