using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RemoveConfigCommand : ConfigCommand
    {
        public RemoveConfigCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveConfigCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}