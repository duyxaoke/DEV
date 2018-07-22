using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class CreateConfigCommandValidation : ConfigValidation<CreateConfigCommand>
    {
        public CreateConfigCommandValidation()
        {
            ValidateCurrency();
            ValidateReferalBonus();
        }
    }
}