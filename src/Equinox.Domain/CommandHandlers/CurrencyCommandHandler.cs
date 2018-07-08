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
    public class CurrencyCommandHandler : CommandHandler,
        IRequestHandler<CreateCurrencyCommand>,
        IRequestHandler<UpdateCurrencyCommand>,
        IRequestHandler<RemoveCurrencyCommand>
    {
        private readonly ICurrencyRepository _CurrencyRepository;
        private readonly IMediatorHandler Bus;

        public CurrencyCommandHandler(ICurrencyRepository CurrencyRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _CurrencyRepository = CurrencyRepository;
            Bus = bus;
        }

        public Task Handle(CreateCurrencyCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var Currency = new Currency(Guid.NewGuid(), message.Code, message.Name, message.Address, message.Quantity, message.IsActive);

            if (_CurrencyRepository.GetByCode(Currency.Code) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Currency Code has already been taken."));
                return Task.CompletedTask;
            }

            _CurrencyRepository.Add(Currency);

            if (Commit())
            {
                Bus.RaiseEvent(new CurrencyCreatedEvent(Currency.Id, Currency.Code, Currency.Name, Currency.Address, Currency.Quantity, Currency.IsActive));
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateCurrencyCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var Currency = new Currency(message.Id, message.Code, message.Name, message.Address, message.Quantity, message.IsActive);
            var existingCurrency = _CurrencyRepository.GetByCode(Currency.Code);

            if (existingCurrency != null && existingCurrency.Id != Currency.Id)
            {
                if (!existingCurrency.Equals(Currency))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Currency Code has already been taken."));
                    return Task.CompletedTask;
                }
            }

            _CurrencyRepository.Update(Currency);

            if (Commit())
            {
                Bus.RaiseEvent(new CurrencyUpdatedEvent(Currency.Id, Currency.Code, Currency.Name, Currency.Address, Currency.Quantity, Currency.IsActive));
            }

            return Task.CompletedTask;
        }

        public Task Handle(RemoveCurrencyCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            _CurrencyRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new CurrencyRemovedEvent(message.Id));
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _CurrencyRepository.Dispose();
        }
    }
}