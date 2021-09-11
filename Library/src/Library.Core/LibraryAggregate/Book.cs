using Library.SharedKernel.Interfaces;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Library.Core.ValueObjects;

namespace Library.Core.LibraryAggregate
{
    public class Book : BaseEntity<Guid>
    {
        public Guid LibraryId { get; set; }

        public ISBN ISBN { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public int NumberOfPages { get; private set; }

        public Guid AuthorId { get; private set; }

        public Genre Genre { get; private set; }

        public BookType Type { get; private set; }

        public int NumberOfCopies { get; private set; }

        public int CurrentAvailableNumberOfCopies { get; private set; }

        public int MinimumNumberOfCopies
        {
            get
            {
                return Type == BookType.Regular ? 1 : 2;
            }
        }

        public Book()
        {

        }

        public Book(Guid id, Guid libraryId, ISBN isbn, string title, string description, int numberOfPages, Guid authorId, Genre genre, BookType type, int numberOfCopies)
        {
            Id = Guard.Against.Default(id, nameof(id));
            LibraryId = Guard.Against.Default(libraryId, nameof(libraryId));
            Title = Guard.Against.NullOrEmpty(title, nameof(title));
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
            NumberOfPages = Guard.Against.NegativeOrZero(numberOfPages, nameof(numberOfPages));
            AuthorId = Guard.Against.Default(authorId, nameof(authorId));
            ISBN = isbn;
            Genre = genre;
            Type = type;
            NumberOfCopies = numberOfCopies;
            CurrentAvailableNumberOfCopies = NumberOfCopies;
        }

        public bool CanLend()
        {
            return CurrentAvailableNumberOfCopies > MinimumNumberOfCopies;
        }

        public void LendBookCopy()
        {
            CurrentAvailableNumberOfCopies--;
        }

        public void ReturnBookCopy()
        {
            CurrentAvailableNumberOfCopies++;
        }
    }
}
