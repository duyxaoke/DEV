using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class CurrencyCreatedEvent : Event
    {
        public CurrencyCreatedEvent(Guid id, string code, string name, string address, decimal? quantity, bool isActive)
        {
            Id = id;
            Code = code;
            Name = name;
            Address = address;
            Quantity = quantity;
            IsActive = isActive;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public decimal? Quantity { get; private set; }
        public bool IsActive { get; private set; }
    }
}