using Equinox.Domain.Validations;
using System;

namespace Equinox.Domain.Commands
{
    public class CreateRefferalCommand : RefferalCommand
    {
        public CreateRefferalCommand(Guid userId, Guid userRefId)
        {
            UserId = userId;
            UserRefId = userRefId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateRefferalCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}