using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class ConfigCreatedEvent : Event
    {
        public ConfigCreatedEvent(Guid id, bool systemEnable, string currency, decimal? referalBonus)
        {
            Id = id;
            SystemEnable = systemEnable;
            Currency = currency;
            ReferalBonus = referalBonus;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public bool SystemEnable { get; private set; }
        public string Currency { get; private set; }
        public decimal? ReferalBonus { get; private set; }
    }
}