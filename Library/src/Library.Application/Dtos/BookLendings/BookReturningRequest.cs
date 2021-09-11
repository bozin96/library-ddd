using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookLendings
{
    public class BookReturningRequest : BaseRequest
    {
        public Guid LibraryId { get; set; }

        public Guid BookLendingId { get; set; }

        public Guid PatronId { get; set; }

        public BookReturningRequest()
        {

        }
    }
}
