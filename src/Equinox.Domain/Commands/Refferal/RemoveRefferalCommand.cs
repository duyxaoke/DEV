using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RemoveRefferalCommand : RefferalCommand
    {
        public RemoveRefferalCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveRefferalCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}