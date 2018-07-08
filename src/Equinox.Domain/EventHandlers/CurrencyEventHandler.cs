using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Events;
using MediatR;

namespace Equinox.Domain.EventHandlers
{
    public class CurrencyEventHandler :
        INotificationHandler<CurrencyCreatedEvent>,
        INotificationHandler<CurrencyUpdatedEvent>,
        INotificationHandler<CurrencyRemovedEvent>
    {
        public Task Handle(CurrencyUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CurrencyCreatedEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CurrencyRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}