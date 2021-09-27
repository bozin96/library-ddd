using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.Books
{
    public class BookAddResponse : BaseResponse
    {
        public BookAddResponse()
        {

        }

        public BookAddResponse(bool status, int statusCode, List<string> errors, object data) : base(status, statusCode, errors, data)
        {
        }
    }
}
