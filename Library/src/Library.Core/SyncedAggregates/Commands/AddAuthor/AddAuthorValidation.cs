using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Commands.AddAuthor
{
    public class AddAuthorValidation : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateDateOfBirth();
        }

        private void ValidateFirstName()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("First name is required.").Length(1, 150)
                .WithMessage("First name must have between 1 and 150 characters.");
        }

        private void ValidateLastName()
        {
            RuleFor(a => a.LastName).NotEmpty().WithMessage("Last name is required.").Length(1, 150)
                .WithMessage("Last name must have between 1 and 150 characters.");
        }

        private void ValidateDateOfBirth()
        {
            RuleFor(a => a.DateOfBirth).NotEmpty().Must(HaveValidDateOfBirth)
                .WithMessage("Author must have al least five years.");
        }

        private static bool HaveValidDateOfBirth(DateTime date)
        {
            return date.Year < DateTime.UtcNow.Year - 5;
        }
    }
}
