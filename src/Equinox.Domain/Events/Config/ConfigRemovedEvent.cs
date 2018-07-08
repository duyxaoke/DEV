using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class ConfigRemovedEvent : Event
    {
        public ConfigRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}