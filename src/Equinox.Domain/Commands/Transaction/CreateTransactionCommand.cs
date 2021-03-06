﻿using Equinox.Domain.Validations;
using System;

namespace Equinox.Domain.Commands
{
    public class CreateTransactionCommand : TransactionCommand
    {
        public CreateTransactionCommand(string userId, int depWithType, decimal? quantity, string iP, int approve, DateTime createdDate, DateTime updatedDate)
        {
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
            ValidationResult = new CreateTransactionCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}