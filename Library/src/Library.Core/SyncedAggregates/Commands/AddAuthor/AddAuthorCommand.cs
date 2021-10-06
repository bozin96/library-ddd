using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Commands.AddAuthor
{
    public class AddAuthorCommand : BaseCommand
    {
        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public DateTime? DateOfDeath { get; private set; }

        public AddAuthorCommand() { }

        public AddAuthorCommand(
           string firstName, string middleName, string lastName, DateTime dateOfBirth, DateTime? dateOfDeath)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DateOfDeath = dateOfDeath;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddAuthorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
