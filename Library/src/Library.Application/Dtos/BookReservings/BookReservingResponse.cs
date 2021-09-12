using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookLendings
{
    public class BookReservingResponse : BaseResponse
    {
        public BookReservingResponse()
        {

        }

        public BookReservingResponse(bool status, int statusCode, List<string> errors, object data) : base(status, statusCode, errors, data)
        {
        }
    }
}
