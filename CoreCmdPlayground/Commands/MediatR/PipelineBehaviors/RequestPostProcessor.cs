using System;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CoreCmdPlayground.Commands.MediatR.PipelineBehaviors
{
    public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        public Task Process(TRequest request, TResponse response)
        {
            Console.WriteLine("+++ GenericRequestPostProcessor<> executed");
            return Task.CompletedTask;
        }
    }
}