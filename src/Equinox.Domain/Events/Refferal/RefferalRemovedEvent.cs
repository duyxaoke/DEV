using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class RefferalRemovedEvent : Event
    {
        public RefferalRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}