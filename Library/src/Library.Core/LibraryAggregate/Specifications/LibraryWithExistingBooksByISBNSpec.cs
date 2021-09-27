using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Specifications
{
    public class LibraryWithExistingBooksByISBNSpec : Specification<Library>, ISingleResultSpecification
    {
        public LibraryWithExistingBooksByISBNSpec(Guid libraryId, string isbn)
        {
            Query
              .Where(l => l.Id == libraryId)
              .Include(l => l.Books.Where(b => b.ISBN.Value == isbn));
        }
    }
}
