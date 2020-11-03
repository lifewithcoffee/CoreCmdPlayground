
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoreCmdPlayground.Commands.MediatR
{
    public class Ping2 : INotification {}

    public class Ping2Handler : INotificationHandler<Ping2>
    {
        public Task Handle(Ping2 notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Executing Ping2Handler.Handle() ...");
            return Task.CompletedTask;
        }
    }
}