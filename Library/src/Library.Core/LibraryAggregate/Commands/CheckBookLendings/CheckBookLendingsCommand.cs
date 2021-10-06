using FluentValidation.Results;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.CheckBookLendings
{
    public class CheckBookLendingsCommand : BaseCommandWithoutResult
    {
        public string Content { get; set; }

        public CheckBookLendingsCommand()
        {
        }

        public CheckBookLendingsCommand(string body)
        {
            Content = body;
        }

        public override bool IsValid()
        {
            ValidationResult = new CheckBookLendingsValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CheckBookLendingsCommandContent
    {
        public Guid LibraryId { get;  set; }

        public Guid PatronId { get;  set; }

        public List<Guid> BookLendingsIds { get;  set; }

        public CheckBookLendingsCommandContent()
        {

        }

        public CheckBookLendingsCommandContent(Guid libraryId, Guid patronId, List<Guid> bookLendingsIds)
        {
            LibraryId = libraryId;
            PatronId = patronId;
            BookLendingsIds = bookLendingsIds;
        }
    }
}
