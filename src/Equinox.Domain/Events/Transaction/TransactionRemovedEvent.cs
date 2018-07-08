using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class TransactionRemovedEvent : Event
    {
        public TransactionRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}