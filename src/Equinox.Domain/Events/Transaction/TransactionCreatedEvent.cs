using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class TransactionCreatedEvent : Event
    {
        public TransactionCreatedEvent(Guid id, string userId, int depWithType, decimal? quantity, string iP, int approve, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            UserId = userId;
            DepWithType = depWithType;
            Quantity = quantity;
            IP = iP;
            Approve = approve;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string UserId { get; private set; }
        public int DepWithType { get; private set; }
        public decimal? Quantity { get; private set; }
        public string IP { get; private set; }
        public int Approve { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
    }
}