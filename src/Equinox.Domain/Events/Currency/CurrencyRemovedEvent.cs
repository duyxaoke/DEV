using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class CurrencyRemovedEvent : Event
    {
        public CurrencyRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}