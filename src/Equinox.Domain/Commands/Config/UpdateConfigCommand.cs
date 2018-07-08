using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class UpdateConfigCommand : ConfigCommand
    {
        public UpdateConfigCommand(Guid id, bool systemEnable, string currency, decimal? referalBonus)
        {
            Id = id;
            SystemEnable = systemEnable;
            Currency = currency;
            ReferalBonus = referalBonus;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateConfigCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}