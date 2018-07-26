using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class UpdateTransactionCommand : TransactionCommand
    {
        public UpdateTransactionCommand(Guid id, string userId, int depWithType, decimal? quantity, string iP, int approve, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            UserId = userId;
            DepWithType = depWithType;
            Quantity = quantity;
            IP = iP;
            Approve = approve;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateTransactionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}