using Equinox.Domain.Validations;
using System;

namespace Equinox.Domain.Commands
{
    public class CreateCurrencyCommand : CurrencyCommand
    {
        public CreateCurrencyCommand(string code, string name, string address, decimal? quantity, bool isActive)
        {
            Code = code;
            Name = name;
            Address = address;
            Quantity = quantity;
            IsActive = isActive;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateCurrencyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}