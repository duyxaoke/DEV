using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands
{
    public abstract class RefferalCommand : Command
    {
        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }
        public Guid UserRefId { get; protected set; }
    }
}