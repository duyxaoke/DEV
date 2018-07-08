using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class CreateConfigCommand : ConfigCommand
    {
        public CreateConfigCommand(bool systemEnable, string currency, decimal? referalBonus)
        {
            SystemEnable = systemEnable;
            Currency = currency;
            ReferalBonus = referalBonus;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateConfigCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}