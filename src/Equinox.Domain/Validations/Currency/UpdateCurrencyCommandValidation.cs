using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class UpdateCurrencyCommandValidation : CurrencyValidation<UpdateCurrencyCommand>
    {
        public UpdateCurrencyCommandValidation()
        {
            ValidateId();
            ValidateCode();
            ValidateName();
        }
    }
}