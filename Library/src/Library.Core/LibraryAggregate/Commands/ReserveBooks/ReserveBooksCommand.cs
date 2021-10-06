using FluentValidation.Results;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.ReserveBooks
{
    public class ReserveBooksCommand : BaseCommand
    {
        public Guid LibraryId { get; set; }

        public List<Guid> BooksIds { get; set; }

        public Guid PatronId { get; set; }

        public ReserveBooksCommand() { }

        public ReserveBooksCommand(Guid libraryId, List<Guid> booksIds, Guid patronId)
        {
            LibraryId = libraryId;
            BooksIds = booksIds;
            PatronId = patronId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ReserveBooksValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
