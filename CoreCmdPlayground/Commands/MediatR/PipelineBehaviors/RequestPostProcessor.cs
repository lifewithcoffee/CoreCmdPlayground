using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CoreCmdPlayground.Commands.MediatR.PipelineBehaviors
{
    public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            Console.WriteLine("+++ GenericRequestPostProcessor<> executed");
            return Task.CompletedTask;
        }
    }
}