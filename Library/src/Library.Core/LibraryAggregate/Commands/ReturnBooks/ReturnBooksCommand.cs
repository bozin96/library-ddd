using FluentValidation.Results;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.ReturnBooks
{
    public class ReturnBooksCommand : BaseCommand<BaseCommandResponse>
    {
        public Guid LibraryId { get; set; }

        public Guid BookReturningId { get; set; }

        public Guid PatronId { get; set; }

        public ReturnBooksCommand() { }

        public ReturnBooksCommand(Guid libraryId, Guid bookReturningId, Guid patronId)
        {
            LibraryId = libraryId;
            BookReturningId = bookReturningId;
            PatronId = patronId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ReturnBooksValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
