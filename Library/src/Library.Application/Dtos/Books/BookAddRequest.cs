using Library.Core.LibraryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.Books
{
    public class BookAddRequest : BaseRequest
    {
        public Guid LibraryId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int NumberOfPages { get; set; }

        public Guid AuthorId { get; set; }

        public Genre Genre { get; set; }

        public BookType Type { get; set; }

        public int NumberOfCopies { get; set; }

        public BookAddRequest()
        {

        }
    }
}
