using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class RefferalCreatedEvent : Event
    {
        public RefferalCreatedEvent(Guid id, Guid userId, Guid userRefId)
        {
            Id = id;
            UserId = userId;
            UserRefId = userRefId;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public Guid UserId { get; private set; }
        public Guid UserRefId { get; private set; }
    }
}