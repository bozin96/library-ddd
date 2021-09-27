using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Commands.AddPatron
{
    public class AddPatronValidation : AbstractValidator<AddPatronCommand>
    {
        public AddPatronValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
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

        private void ValidateEmail()
        {
            RuleFor(a => a.Email).NotEmpty().EmailAddress();
        }
    }
}
