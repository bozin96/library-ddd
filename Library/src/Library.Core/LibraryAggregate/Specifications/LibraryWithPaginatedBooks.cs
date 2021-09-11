using Ardalis.Specification;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Specifications
{
    public class LibraryWithPaginatedBooks : Specification<Library>, ISingleResultSpecification
    {
        public LibraryWithPaginatedBooks(Guid libraryId, ResourceParameters resorceParameters)
        {
            Query
              .Where(l => l.Id == libraryId)
              .Include(l => l.Books.Skip((resorceParameters.PageNumber - 1) * resorceParameters.PageSize)
                                   .Take(resorceParameters.PageSize));
        }
    }
}
