using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Events.BookLended
{
    public class BookLentEvent : BaseDomainEvent
    {
        public BookLentEvent(Guid bookId, Guid patronId)
        {
            BookId = bookId;
            PatronId = patronId;
        }

        public Guid BookId { get; private set; }

        public Guid PatronId { get; private set; }
    }
}
