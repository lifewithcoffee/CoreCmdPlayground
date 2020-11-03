
using FluentValidation;

namespace CoreCmdPlayground.Commands.MediatR
{
    public class PingRequestValidator : AbstractValidator<Ping>
    {
        public PingRequestValidator()
        {
            //RuleFor(x => x.SomeProp).NotNull();
        }
    }
}