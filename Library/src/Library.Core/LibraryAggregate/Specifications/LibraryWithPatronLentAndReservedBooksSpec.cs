using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Core.LibraryAggregate.Specifications
{
    public class LibraryWithPatronLentAndReservedBooksSpec : Specification<Library>, ISingleResultSpecification
    {
        public LibraryWithPatronLentAndReservedBooksSpec(Guid libraryId, Guid patronId, List<Guid> booksIds)
        {
            Query
              .Where(l => l.Id == libraryId)
              .Include(l => l.Books.Where(b => booksIds.Contains(b.Id)))
              .Include(l => l.BookLendings.Where(bl => bl.PatronId == patronId && bl.Active))
              .Include(l => l.BookReservations.Where(bl => bl.PatronId == patronId && bl.Active));

        }
    }
}
