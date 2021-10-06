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
        public BookReservedEvent(Guid bookId, Guid patronId)
        {
            BookId = bookId;
            PatronId = patronId;
        }

        public Guid BookId { get; set; }

        public Guid PatronId { get; set; }
    }
}
