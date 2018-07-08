using System;
using Equinox.Domain.Commands;
using FluentValidation;

namespace Equinox.Domain.Validations
{
    public abstract class ConfigValidation<T> : AbstractValidator<T> where T : ConfigCommand
    {
        protected void ValidateSystemEnable()
        {
            RuleFor(c => c.SystemEnable)
                .NotEmpty();
        }

        protected void ValidateCurrency()
        {
            RuleFor(c => c.Currency)
                .NotEmpty()
                .Matches("^[0-9a-zA-Z ']*$")
              .WithMessage("Name must contain characters, numbers and spaces only")
                .Length(2, 150);
        }

        protected void ValidateReferalBonus()
        {
            RuleFor(c => c.ReferalBonus)
                .NotEmpty()
                .ExclusiveBetween(0, 100);
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}