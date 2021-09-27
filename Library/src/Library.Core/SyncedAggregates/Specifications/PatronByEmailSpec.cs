using Ardalis.Specification;
using Library.Core.SyncedAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Specifications
{
    public class PatronByEmailSpec : Specification<Patron>, ISingleResultSpecification
    {
        public PatronByEmailSpec(string email)
        {
            Query
              .Where(p => p.Email == email);
        }
    }
}
