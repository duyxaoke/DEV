using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RemoveTransactionCommandValidation : TransactionValidation<RemoveTransactionCommand>
    {
        public RemoveTransactionCommandValidation()
        {
            ValidateId();
        }
    }
}