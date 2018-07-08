using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands
{
    public abstract class ConfigCommand : Command
    {
        public Guid Id { get; protected set; }
        public bool SystemEnable { get; protected set; }
        public string Currency { get; protected set; }
        public decimal? ReferalBonus { get; protected set; }
    }
}