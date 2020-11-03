using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CoreCmdPlayground.Commands.MediatR.PipelineBehaviors
{
    public class GenericRequestPreprocessor2<T> : IRequestPreProcessor<T>
    {
        public Task Process(T request, CancellationToken cancellationToken)
        {
            Console.WriteLine("~~2 GenericRequestPreprocessor2<> executed");
            return Task.CompletedTask;
        }
    }

    public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("~~1 GenericRequestPreProcessor<> executed");
            return Task.CompletedTask;
        }
    }

    /** don't do this:
     *  an exception "System.ArgumentException" will be thrown out in runtime
     */
    //public class PingPipelinePreProcess : IRequestPreProcessor<Ping>
    //{
    //    public Task Process(Ping request, CancellationToken cancellationToken)
    //    {
    //        Console.WriteLine($"{typeof(PingPipelinePreProcess).Name}.Process() called");
    //        return Task.CompletedTask;
    //    }
    //}
}