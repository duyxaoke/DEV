using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class CreateCurrencyCommandValidation : CurrencyValidation<CreateCurrencyCommand>
    {
        public CreateCurrencyCommandValidation()
        {
            ValidateCode();
            ValidateName();
        }
    }
}