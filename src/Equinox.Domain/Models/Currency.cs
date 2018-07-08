using System;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Currency : Entity
    {
        public Currency(Guid id, string code, string name, string address, decimal? quantity, bool isActive)
        {
            Id = id;
            Code = code;
            Name = name;
            Address = address;
            Quantity = quantity;
            IsActive = isActive;
        }

        // Empty constructor for EF
        protected Currency() { }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public decimal? Quantity { get; private set; }
        public bool IsActive { get; private set; }
    }
}