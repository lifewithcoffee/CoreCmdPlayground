using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using CoreCmdPlayground.Commands.MediatR.PipelineBehaviors;
using System;

namespace CoreCmdPlayground.Commands.MediatR
{
    public class Ping : IRequest<string>
    { 
        public string SomeProp { get; set; }
    }
    
    public class PingHandler : IRequestHandler<Ping, string>
    {
        readonly IValidator<Ping> _validator;

        public PingHandler(IValidator<Ping> validator)
        {
            _validator = validator;
        }

        public async Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            var results = await _validator.ValidateAsync(request);
            if(!results.IsValid)
            {
                foreach(var failure in results.Errors)
                {
                    Console.WriteLine("_______ >>> Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }

                var failures = results.Errors.ToList();
                throw new MyValidationException(failures);
            }

            Console.WriteLine("Executing PingHandler.Handle() ...");
            return await Task.FromResult("Pong");
        }
    }
}