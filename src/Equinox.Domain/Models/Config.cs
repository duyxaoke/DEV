using System;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Config : Entity
    {
        public Config(Guid id, bool systemEnable, string currency, decimal? referalBonus)
        {
            Id = id;
            SystemEnable = systemEnable;
            Currency = currency;
            ReferalBonus = referalBonus;
        }

        // Empty constructor for EF
        protected Config() { }

        public bool SystemEnable { get; private set; }
        public string Currency { get; private set; }
        public decimal? ReferalBonus { get; private set; }
    }
}