using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class CreateRefferalCommandValidation : RefferalValidation<CreateRefferalCommand>
    {
        public CreateRefferalCommandValidation()
        {
            ValidateUserId();
            ValidateUserRefId();
        }
    }
}