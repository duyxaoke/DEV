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
    public class RefferalCommandHandler : CommandHandler,
        IRequestHandler<CreateRefferalCommand>,
        IRequestHandler<RemoveRefferalCommand>
    {
        private readonly IRefferalRepository _RefferalRepository;
        private readonly IMediatorHandler Bus;

        public RefferalCommandHandler(IRefferalRepository RefferalRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _RefferalRepository = RefferalRepository;
            Bus = bus;
        }

        public Task Handle(CreateRefferalCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var Refferal = new Refferal(Guid.NewGuid(), message.UserId, message.UserRefId);

            _RefferalRepository.Add(Refferal);

            if (Commit())
            {
                Bus.RaiseEvent(new RefferalCreatedEvent(Refferal.Id, Refferal.UserId, Refferal.UserRefId));
            }

            return Task.CompletedTask;
        }

        public Task Handle(RemoveRefferalCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            _RefferalRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new RefferalRemovedEvent(message.Id));
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _RefferalRepository.Dispose();
        }
    }
}