using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RemoveRefferalCommandValidation : RefferalValidation<RemoveRefferalCommand>
    {
        public RemoveRefferalCommandValidation()
        {
            ValidateId();
        }
    }
}