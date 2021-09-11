using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookLendings
{
    public class BookLendingResponse : BaseResponse
    {
        public BookLendingResponse()
        {

        }

        public BookLendingResponse(bool status, int statusCode, List<string> errors, object data) : base(status, statusCode, errors, data)
        {
        }
    }
}
