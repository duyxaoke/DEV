using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Events;
using MediatR;

namespace Equinox.Domain.EventHandlers
{
    public class RefferalEventHandler :
        INotificationHandler<RefferalCreatedEvent>,
        INotificationHandler<RefferalRemovedEvent>
    {
        public Task Handle(RefferalCreatedEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(RefferalRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}