using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreCmdPlayground.Commands.MediatR
{
    public class MediatrCommand
    {
        readonly IMediator _mediator;

        public MediatrCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Request()
        {
            var result = await _mediator.Send(new Ping());
            Console.WriteLine(result);
        }

        public async Task Publish()
        {
            await _mediator.Publish(new Ping2());
        }
    }
}
