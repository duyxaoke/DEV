using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RemoveCurrencyCommand : CurrencyCommand
    {
        public RemoveCurrencyCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCurrencyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}