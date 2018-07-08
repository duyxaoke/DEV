using System;
using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Events;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using MediatR;

namespace Equinox.Domain.CommandHandlers
{
    public class ConfigCommandHandler : CommandHandler,
        IRequestHandler<CreateConfigCommand>,
        IRequestHandler<UpdateConfigCommand>,
        IRequestHandler<RemoveConfigCommand>
    {
        private readonly IConfigRepository _ConfigRepository;
        private readonly IMediatorHandler Bus;

        public ConfigCommandHandler(IConfigRepository ConfigRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _ConfigRepository = ConfigRepository;
            Bus = bus;
        }

        public Task Handle(CreateConfigCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var Config = new Config(Guid.NewGuid(), message.SystemEnable, message.Currency, message.ReferalBonus);

            if (_ConfigRepository.GetExist())
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Config has already been taken."));
                return Task.CompletedTask;
            }

            _ConfigRepository.Add(Config);

            if (Commit())
            {
                Bus.RaiseEvent(new ConfigCreatedEvent(Config.Id, Config.SystemEnable, Config.Currency, Config.ReferalBonus));
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateConfigCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var Config = new Config(message.Id, message.SystemEnable, message.Currency, message.ReferalBonus);
            _ConfigRepository.Update(Config);

            if (Commit())
            {
                Bus.RaiseEvent(new ConfigUpdatedEvent(Config.Id, Config.SystemEnable, Config.Currency, Config.ReferalBonus));
            }

            return Task.CompletedTask;
        }

        public Task Handle(RemoveConfigCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            _ConfigRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new ConfigRemovedEvent(message.Id));
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _ConfigRepository.Dispose();
        }
    }
}