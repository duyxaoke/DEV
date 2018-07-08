using System;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class RefferalHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public Guid UserRefId { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}