using System;
using Equinox.Domain.Commands;
using FluentValidation;

namespace Equinox.Domain.Validations
{
    public abstract class TransactionValidation<T> : AbstractValidator<T> where T : TransactionCommand
    {
        protected void ValidateUserId()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Please ensure you have entered the UserId");
        }

        protected void ValidateDepWithType()
        {
            RuleFor(c => c.DepWithType)
                .ExclusiveBetween(0, 10);
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}