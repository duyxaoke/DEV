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
    public class TransactionCommandHandler : CommandHandler,
        IRequestHandler<CreateTransactionCommand>,
        IRequestHandler<UpdateTransactionCommand>,
        IRequestHandler<RemoveTransactionCommand>
    {
        private readonly ITransactionRepository _TransactionRepository;
        private readonly IMediatorHandler Bus;

        public TransactionCommandHandler(ITransactionRepository TransactionRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _TransactionRepository = TransactionRepository;
            Bus = bus;
        }

        public Task Handle(CreateTransactionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var Transaction = new Transaction(Guid.NewGuid(), message.UserId, message.DepWithType, message.Quantity,
                message.IP, message.Approve, message.CreatedDate, message.UpdatedDate);

            _TransactionRepository.Add(Transaction);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionCreatedEvent(Transaction.Id, Transaction.UserId, Transaction.DepWithType, Transaction.Quantity,
                    Transaction.IP, Transaction.Approve, Transaction.CreatedDate, Transaction.UpdatedDate));
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateTransactionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var Transaction = new Transaction(message.Id, message.UserId, message.DepWithType, message.Quantity,
                message.IP, message.Approve, message.CreatedDate, message.UpdatedDate);
            _TransactionRepository.Update(Transaction);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionUpdatedEvent(Transaction.Id, Transaction.UserId, Transaction.DepWithType, Transaction.Quantity,
                    Transaction.IP, Transaction.Approve, Transaction.CreatedDate, Transaction.UpdatedDate));
            }

            return Task.CompletedTask;
        }

        public Task Handle(RemoveTransactionCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            _TransactionRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionRemovedEvent(message.Id));
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _TransactionRepository.Dispose();
        }
    }
}