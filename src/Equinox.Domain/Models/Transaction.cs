using System;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Transaction : Entity
    {
        public Transaction(Guid id, Guid userId, int depWithType, decimal? quantity, string iP, bool approve, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            UserId = userId;
            DepWithType = depWithType;
            Quantity = quantity;
            IP = iP;
            Approve = approve;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }

        // Empty constructor for EF
        protected Transaction() { }

        public Guid UserId { get; private set; }
        public int DepWithType { get; private set; }
        public decimal? Quantity { get; private set; }
        public string IP { get; private set; }
        public bool Approve { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
    }
}