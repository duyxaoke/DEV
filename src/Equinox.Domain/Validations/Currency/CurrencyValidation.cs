using System;
using Equinox.Domain.Commands;
using FluentValidation;

namespace Equinox.Domain.Validations
{
    public abstract class CurrencyValidation<T> : AbstractValidator<T> where T : CurrencyCommand
    {
        protected void ValidateCode()
        {
            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("Please ensure you have entered the Code")
                .Length(2, 50).WithMessage("The Code must have between 2 and 50 characters")
                .Matches("^[0-9a-zA-Z ']*$")
                .WithMessage("Name must contain characters, numbers and spaces only");
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters")
                .Matches("^[0-9a-zA-Z ']*$")
                .WithMessage("Name must contain characters, numbers and spaces only");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}