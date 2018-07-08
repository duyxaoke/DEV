using System;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Refferal : Entity
    {
        public Refferal(Guid id, Guid userId, Guid userRefId)
        {
            Id = id;
            UserId = userId;
            UserRefId = userRefId;
        }

        // Empty constructor for EF
        protected Refferal() { }

        public Guid UserId { get; private set; }
        public Guid UserRefId { get; private set; }
    }
}