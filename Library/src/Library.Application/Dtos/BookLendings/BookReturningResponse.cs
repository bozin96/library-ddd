using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookLendings
{
    public class BookReturningResponse : BaseResponse
    {
        public BookReturningResponse()
        {

        }

        public BookReturningResponse(bool status, int statusCode, List<string> errors, object data) : base(status, statusCode, errors, data)
        {
        }
    }
}
