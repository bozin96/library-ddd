using Ardalis.Specification;
using Library.Core.SyncedAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.SyncedAggregates.Specifications
{
    public class AuthorByFullNameSpec : Specification<Author>, ISingleResultSpecification
    {
        public AuthorByFullNameSpec(string firstName, string middleName, string lastName)
        {
            Query
              .Where(a => a.FirstName.ToLower().Trim() == firstName.ToLower().Trim() &&
                         a.MiddleName.ToLower().Trim() == middleName.ToLower().Trim() &&
                         a.LastName.ToLower().Trim() == lastName.ToLower().Trim());
        }
    }
}