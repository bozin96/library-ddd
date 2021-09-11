using FluentValidation.Results;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.BookReturning
{
    public class BookReturningCommand : BaseCommand<BaseCommandResponse>
    {
        public Guid LibraryId { get; set; }

        public Guid BookLendingId { get; set; }

        public Guid PatronId { get; set; }

        public BookReturningCommand() { }

        public BookReturningCommand(Guid libraryId, Guid bookLendingId, Guid patronId)
        {
            LibraryId = libraryId;
            BookLendingId = BookLendingId;
            PatronId = patronId;
        }

        public override bool IsValid()
        {
            ValidationResult validationResult = new BookReturningValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
