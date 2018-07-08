using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Events;
using MediatR;

namespace Equinox.Domain.EventHandlers
{
    public class ConfigEventHandler :
        INotificationHandler<ConfigCreatedEvent>,
        INotificationHandler<ConfigUpdatedEvent>,
        INotificationHandler<ConfigRemovedEvent>
    {
        public Task Handle(ConfigCreatedEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(ConfigUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(ConfigRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}