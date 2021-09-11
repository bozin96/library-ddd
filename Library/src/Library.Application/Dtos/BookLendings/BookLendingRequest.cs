using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookLendings
{
    public class BookLendingRequest : BaseRequest
    {
        public Guid LibraryId { get; set; }
        
        public List<Guid> BooksIds { get; set; }

        public Guid PatronId { get; set; }

        public BookLendingRequest()
        {

        }
    }
}
