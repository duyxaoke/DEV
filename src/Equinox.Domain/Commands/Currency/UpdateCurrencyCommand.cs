using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class UpdateCurrencyCommand : CurrencyCommand
    {
        public UpdateCurrencyCommand(Guid id, string code, string name, string address, decimal? quantity, bool isActive)
        {
            Id = id;
            Code = code;
            Name = name;
            Address = address;
            Quantity = quantity;
            IsActive = isActive;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCurrencyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}