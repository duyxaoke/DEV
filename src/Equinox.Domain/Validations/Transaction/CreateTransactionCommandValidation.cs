using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class CreateTransactionCommandValidation : TransactionValidation<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidation()
        {
            ValidateUserId();
            ValidateDepWithType();
        }
    }
}