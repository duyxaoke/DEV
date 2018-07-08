using System;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class TransactionHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public int DepWithType { get; set; }
        public decimal? Quantity { get; set; }
        public string IP { get; set; }
        public bool Approve { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}