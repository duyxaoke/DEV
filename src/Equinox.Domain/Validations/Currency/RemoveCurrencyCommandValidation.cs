using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RemoveCurrencyCommandValidation : CurrencyValidation<RemoveCurrencyCommand>
    {
        public RemoveCurrencyCommandValidation()
        {
            ValidateId();
        }
    }
}