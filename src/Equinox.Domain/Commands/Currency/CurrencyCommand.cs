using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands
{
    public abstract class CurrencyCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string Address { get; protected set; }
        public decimal? Quantity { get; protected set; }
        public bool IsActive { get; protected set; }
    }
}