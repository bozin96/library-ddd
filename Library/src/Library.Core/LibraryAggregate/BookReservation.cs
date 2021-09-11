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
    public class BookReservation : BaseEntity<Guid>
    {
        public Guid PatronId { get; private set; }

        public Guid BookId { get; private set; }

        public DateTimeRange DateTimeRange { get; private set; }

        public bool Active { get; private set; }

        public BookReservation() { }

        public BookReservation(Guid patronId, Guid bookId, DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
                throw new ArgumentOutOfRangeException(nameof(endDate), "Reservation end date must be greater than start date.");


            if (endDate < DateTime.Now)
                throw new ArgumentOutOfRangeException(nameof(endDate), "Reservation end date must be later than now.");

            PatronId = Guard.Against.Default(patronId, nameof(patronId));
            BookId = Guard.Against.Default(bookId, nameof(bookId)); ;

            startDate = startDate.ToUniversalTime();
            endDate = endDate.ToUniversalTime();
            DateTimeRange = new DateTimeRange(
                Guard.Against.Default(startDate, nameof(startDate)),
                Guard.Against.Default(endDate, nameof(endDate)));
            Active = true;
        }

        public void CloseReservation()
        {
            Active = false;
        }
    }
}
