using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Core.LibraryAggregate.Specifications
{
    public class LibraryWithPatronLentBooksSpec : Specification<Library>, ISingleResultSpecification
    {
        public LibraryWithPatronLentBooksSpec(Guid libraryId, Guid patronId, Guid bookLendingId)
        {
            Query
              .Where(l => l.Id == libraryId)
              .Include(l => l.BookLendings.Where(bl => bl.Id == bookLendingId))
              .ThenInclude(bl => bl.Book);
        }
    }
}
