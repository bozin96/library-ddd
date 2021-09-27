using Ardalis.Specification;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Specifications
{
    public class LibraryWithPaginatedBooksSpec : Specification<Library>, ISingleResultSpecification
    {
        public LibraryWithPaginatedBooksSpec(Guid libraryId, ResourceParameters resorceParameters)
        {
            Query
              .Where(l => l.Id == libraryId)
              .Include(l => l.Books.Skip((resorceParameters.PageNumber - 1) * resorceParameters.PageSize)
                                   .Take(resorceParameters.PageSize));
        }
    }
}
