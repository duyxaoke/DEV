using System;
using Equinox.Domain.Commands;
using FluentValidation;

namespace Equinox.Domain.Validations
{
    public abstract class RefferalValidation<T> : AbstractValidator<T> where T : RefferalCommand
    {
        protected void ValidateUserId()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Please ensure you have entered the UserId");
        }

        protected void ValidateUserRefId()
        {
            RuleFor(c => c.UserRefId)
                .NotEmpty().WithMessage("Please ensure you have entered the UserRefId");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}