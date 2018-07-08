using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands
{
    public abstract class TransactionCommand : Command
    {
        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }
        public int DepWithType { get; protected set; }
        public decimal? Quantity { get; protected set; }
        public string IP { get; protected set; }
        public bool Approve { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime UpdatedDate { get; protected set; }
    }
}