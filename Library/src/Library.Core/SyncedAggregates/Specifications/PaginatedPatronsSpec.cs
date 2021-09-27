using Ardalis.Specification;
using Library.Core.SyncedAggregate;
using Library.SharedKernel;
using System.Linq;

namespace Library.Core.SyncedAggregates.Specifications
{
    public class PaginatedPatronsSpec : Specification<Patron>
    {
        public PaginatedPatronsSpec(ResourceParameters resorceParameters)
        {
            Query
              .Skip((resorceParameters.PageNumber - 1) * resorceParameters.PageSize)
              .Take(resorceParameters.PageSize);
        }
    }
}
