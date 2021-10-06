using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Specifications
{
    class LibraryWithPatronActiveLentBooksSpec : Specification<Library>, ISingleResultSpecification
    {
        public LibraryWithPatronActiveLentBooksSpec(Guid libraryId, Guid patronId)
        {
            Query
              .Where(l => l.Id == libraryId)
              .Include(l => l.BookLendings.Where(bl => bl.PatronId == patronId && bl.Active));
        }
    }
}
