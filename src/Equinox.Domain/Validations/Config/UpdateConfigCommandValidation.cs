using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class UpdateConfigCommandValidation : ConfigValidation<UpdateConfigCommand>
    {
        public UpdateConfigCommandValidation()
        {
            ValidateId();
            ValidateSystemEnable();
            ValidateCurrency();
            ValidateReferalBonus();
        }
    }
}