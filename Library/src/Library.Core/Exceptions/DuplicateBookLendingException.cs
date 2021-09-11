using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Exceptions
{
    public class DuplicateBookLendingException : ArgumentException
    {
        public DuplicateBookLendingException(string message, string paramName) : base(message, paramName)
        {

        }
    }
}
