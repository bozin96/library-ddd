using Library.Core.SyncedAggregate;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Commands.AddPatron
{
    public class AddPatronCommand : BaseCommand
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public PatronType Type { get; private set; }

        public AddPatronCommand() { }

        public AddPatronCommand(
           string firstName, string middleName, string lastName, string email, PatronType type)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Type = type;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddPatronValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
