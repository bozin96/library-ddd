using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Events.BookReserved
{
    public class BookReservedEvent : BaseDomainEvent
    {
        public BookReservedEvent(BookReservation bookReservation)
        {
            BookReservation = bookReservation;
        }

        public BookReservation BookReservation { get; set; }
    }
}
