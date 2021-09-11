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
        public BookLentEvent(BookLending bookLending)
        {
            BookLending = bookLending;
        }

        public BookLending BookLending { get; set; }
    }
}
