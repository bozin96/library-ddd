using Ardalis.GuardClauses;
using Library.Core.LibraryAggregate.Events.BookLended;
using Library.Core.LibraryAggregate.Guards;
using Library.Core.LibraryAggregate;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Library.Core.SyncedAggregate;
using Library.Core.LibraryAggregate.Events.BookReserved;

namespace Library.Core.LibraryAggregate
{
    public class Library : BaseEntity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; }


        private readonly List<Book> _books = new List<Book>();
        public IEnumerable<Book> Books => _books.AsReadOnly();

        private readonly List<BookLending> _bookLendings = new List<BookLending>();
        public IEnumerable<BookLending> BookLendings => _bookLendings.AsReadOnly();

        private readonly List<BookReservation> _bookReservations = new List<BookReservation>();
        public IEnumerable<BookReservation> BookReservations => _bookReservations.AsReadOnly();

        public Library() { }

        public Library(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        // Izbaci bolje ovo jer ovo moze da pozove od negde bez one prethodne validacije
        public BookLending AddNewBookLending(BookLending bookLending)
        {
            Guard.Against.Null(bookLending, nameof(bookLending));
            Guard.Against.Default(bookLending.Id, nameof(bookLending.Id));
            Guard.Against.DuplicateBookLending(_bookLendings, bookLending, nameof(bookLending));

            _bookLendings.Add(bookLending);

            BookLentEvent bookLendedEvent = new BookLentEvent(bookLending);
            Events.Add(bookLendedEvent);

            return bookLending;
        }

        public BookReservation AddNewBookReservation(BookReservation bookReservation)
        {
            Guard.Against.Null(bookReservation, nameof(bookReservation));
            Guard.Against.Default(bookReservation.Id, nameof(bookReservation.Id));
            Guard.Against.DuplicateBookReservation(_bookReservations, bookReservation, nameof(bookReservation));

            _bookReservations.Add(bookReservation);

            BookReservedEvent bookReservedEvent = new BookReservedEvent(bookReservation);
            Events.Add(bookReservedEvent);

            return bookReservation;
        }

        public DomainActionStatus CanPatronLendBooks(Patron patron, List<Guid> booksToLendIds)
        {
            List<BookLending> patronsAlreadyLentBooks = BookLendings
                    .Where(bl => bl.PatronId == patron.Id && bl.Active).ToList();

            List<Book> booksToLend = Books
                    .Where(b => booksToLendIds.Contains(b.Id)).ToList();

            // If some book is not available at the moment.
            foreach (Book book in booksToLend)
            {
                if (book.MinimumNumberOfCopies >= book.CurrentAvailableNumberOfCopies)
                {
                    return new DomainActionStatus("Some book is not available at the moment.", StatusCodes.Status409Conflict);
                }
            }

            int countAlreadyLentBooks = patronsAlreadyLentBooks.Count;
            int countNewBooksToLend = booksToLendIds.Count;

            // If Patron can not lend more books.
            if (patron.MaxNumberOfLendedBooksAtSameTime < countAlreadyLentBooks + countNewBooksToLend)
            {
                return new DomainActionStatus("You exceeded maximum number of lent books.", StatusCodes.Status409Conflict);
            }

            // If Patron already lent some book from list of new books.
            if (patronsAlreadyLentBooks.Any(bl => booksToLendIds.Contains(bl.Id)))
            {
                return new DomainActionStatus("You already lent book from entered list.", StatusCodes.Status409Conflict);
            }

            return new DomainActionStatus();
        }

        public DomainActionStatus CanPatronReserveBooks(Patron patron, List<Guid> booksToReserveIds)
        {
            List<BookLending> patronsAlreadyLentBooks = BookLendings
                .Where(bl => bl.PatronId == patron.Id && bl.Active).ToList();

            List<BookReservation> patronsAlreadyReservedBooks = BookReservations
                .Where(bl => bl.PatronId == patron.Id && bl.Active).ToList();

            List<Book> booksToLend = Books
                .Where(b => booksToReserveIds.Contains(b.Id)).ToList();

            // If some book is not available at the moment it cannot be reserved.
            foreach (Book book in booksToLend)
            {
                if (book.MinimumNumberOfCopies >= book.CurrentAvailableNumberOfCopies)
                {
                    return new DomainActionStatus("Some book is not available at the moment.", StatusCodes.Status409Conflict);
                }
            }

            int countAlreadyReservedBooks = patronsAlreadyReservedBooks.Count;
            int countNewBooksToReserve = booksToReserveIds.Count;

            // If Patron can not reserve more books.
            if (patron.MaxNumberOfReservedBooksAtSameTime < countAlreadyReservedBooks + countNewBooksToReserve)
            {
                return new DomainActionStatus("You exceeded maximum number of lent books.", StatusCodes.Status409Conflict);
            }

            // If Patron already lent some book from list of new books that book cannot be reserved.
            if (patronsAlreadyLentBooks.Any(bl => booksToReserveIds.Contains(bl.Id)))
            {
                return new DomainActionStatus("You already lent book from entered list.", StatusCodes.Status409Conflict);
            }

            // If Patron already reserved some book from list of new books that book cannot be reserved.
            if (patronsAlreadyReservedBooks.Any(bl => booksToReserveIds.Contains(bl.Id)))
            {
                return new DomainActionStatus("You already reserved book from entered list.", StatusCodes.Status409Conflict);
            }

            return new DomainActionStatus();
        }
    }
}
