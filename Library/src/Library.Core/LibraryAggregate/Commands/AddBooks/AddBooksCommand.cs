using FluentValidation.Results;
using Library.Core.LibraryAggregate.Commands.LendBooks;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Commands.AddBooks
{
    public class AddBooksCommand : BaseCommand<BaseCommandResponse>
    {
        public Guid LibraryId { get; private set; }

        public string ISBN { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public int NumberOfPages { get; private set; }

        public Guid AuthorId { get; private set; }

        public Genre Genre { get; private set; }

        public BookType Type { get; private set; }

        public int NumberOfCopies { get; private set; }

        public AddBooksCommand() { }

        public AddBooksCommand(
            Guid libraryId, string isbn, string title, string description, int numberOfPages, Guid authorId, Genre genre, BookType type, int numberOfCopies)
        {
            LibraryId = libraryId;
            ISBN = isbn;
            Title = title;
            Description = description;
            NumberOfPages = numberOfPages;
            AuthorId = authorId;
            Genre = genre;
            Type = type;
            NumberOfCopies = numberOfCopies;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddBooksValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
