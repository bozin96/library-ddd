using Ardalis.GuardClauses;
using Library.Core.LibraryAggregate;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate
{
    public class BookLending : BaseEntity<Guid>
    {
        public Guid PatronId { get; private set; }

        public Book Book { get; private set; }

        public Guid BookId { get; private set; }

        public DateTimeRange DateTimeRange { get; private set; }

        public bool Active { get; private set; }

        public BookLending() { }

        public BookLending(Guid id, Guid patronId, Book book, DateTime endDate)
        {
            if (endDate < DateTime.Now)
                throw new ArgumentOutOfRangeException(nameof(endDate), "Rental end date must be later than now.");

            Id = Guard.Against.Default(id, nameof(id));
            PatronId = Guard.Against.Default(patronId, nameof(patronId));
            BookId = Guard.Against.Default(book.Id, nameof(book.Id));
            endDate = endDate.ToUniversalTime();
            DateTimeRange = new DateTimeRange(DateTime.UtcNow, Guard.Against.Default(endDate, nameof(endDate)));
            Active = true;
        }

        public bool IsExpired()
        {
            return DateTimeRange.End.Date < DateTime.UtcNow.Date;
        }

        public void CloseLending()
        {
            Active = false;
        }
    }
}
