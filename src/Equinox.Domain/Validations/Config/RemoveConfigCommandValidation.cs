using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RemoveConfigCommandValidation : ConfigValidation<RemoveConfigCommand>
    {
        public RemoveConfigCommandValidation()
        {
            ValidateId();
        }
    }
}