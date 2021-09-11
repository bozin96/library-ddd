using Ardalis.GuardClauses;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.LibraryAggregate.Guards
{
    public static class LibraryGuardExtensions
    {
        public static void DuplicateBookLending(this IGuardClause guardClause, IEnumerable<BookLending> existingBookLendings, BookLending bookLending, string parameterName)
        {
            if (existingBookLendings.Any(bl => bl.Id == bookLending.Id))
            {
                throw new DuplicateBookLendingException("Cannot add duplicate book lending.", parameterName);
            }
        }
    }
}
