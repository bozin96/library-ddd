using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Library.Core.LibraryAggregate.Commands.LendBooks
{
    public class LendBooksCommand : BaseCommand<BaseCommandResponse>
    {
        public Guid LibraryId { get; set; }

        public List<Guid> BooksIds { get; set; }

        public Guid PatronId { get; set; }

        public LendBooksCommand() { }

        public LendBooksCommand(Guid libraryId, List<Guid> booksIds, Guid patronId)
        {
            LibraryId = libraryId;
            BooksIds = booksIds;
            PatronId = patronId;
        }

        public override bool IsValid()
        {
            ValidationResult  = new LendBooksValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
