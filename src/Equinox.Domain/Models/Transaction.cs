using System;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Transaction : Entity
    {
        public Transaction(Guid id, string userId, int depWithType, decimal? quantity, string iP, int approve, DateTime createdDate, DateTime updatedDate)
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

        public string UserId { get; private set; }
        public int DepWithType { get; private set; }
        public decimal? Quantity { get; private set; }
        public string IP { get; private set; }
        public int Approve { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
    }
}