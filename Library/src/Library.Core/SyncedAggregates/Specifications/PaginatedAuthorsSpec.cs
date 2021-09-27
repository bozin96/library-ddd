using Ardalis.Specification;
using Library.Core.SyncedAggregate;
using Library.SharedKernel;
using System.Collections.Generic;
using System.Linq;

namespace Library.Core.SyncedAggregates.Specifications
{
    public class PaginatedAuthorsSpec : Specification<Author>
    {
        public PaginatedAuthorsSpec(ResourceParameters resorceParameters)
        {
            Query
              .Skip((resorceParameters.PageNumber - 1) * resorceParameters.PageSize)
              .Take(resorceParameters.PageSize);
        }
    }
}
