using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoreCmdPlayground.Commands.MediatR.PipelineBehaviors
{
    public class LoggingBehavior2<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine("LoggingBehavior2 --- start");
            var response = await next();
            Console.WriteLine("LoggingBehavior2 --- finish");
            return response;
        }
    }

    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine("LoggingBehavior ... begin");
            var response = await next();
            Console.WriteLine("LoggingBehavior ... end");
            return response;
        }
    }

    public class PingPipelineBehavior : IPipelineBehavior<Ping, string>
    {
        public async Task<string> Handle(Ping request, RequestHandlerDelegate<string> next, CancellationToken cancellationToken)
        {
            Console.WriteLine($">>> Start to handle {typeof(PingPipelineBehavior).Name}");
            var response = await next();
            Console.WriteLine($"<<< End of Handling {typeof(PingPipelineBehavior).Name}");
            return response;
        }
    }
}