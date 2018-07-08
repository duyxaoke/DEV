using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class UpdateTransactionCommandValidation : TransactionValidation<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidation()
        {
            ValidateId();
            ValidateUserId();
            ValidateDepWithType();
        }
    }
}