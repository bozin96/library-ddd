using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Library.Core.LibraryAggregate.Commands.BookLending
{
    public class BookLendingCommand : BaseCommand<BaseCommandResponse>
    {
        public Guid LibraryId { get; set; }

        public List<Guid> BooksIds { get; set; }

        public Guid PatronId { get; set; }

        public BookLendingCommand() { }

        public BookLendingCommand(Guid libraryId, List<Guid> booksIds, Guid patronId)
        {
            LibraryId = libraryId;
            BooksIds = booksIds;
            PatronId = patronId;
        }

        public override bool IsValid()
        {
            ValidationResult validationResult = new BookLendingValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
