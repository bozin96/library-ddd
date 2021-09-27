using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.Authors
{
    public class AuthorAddResponse : BaseResponse
    {
        public AuthorAddResponse()
        {

        }

        public AuthorAddResponse(bool status, int statusCode, List<string> errors, object data) : base(status, statusCode, errors, data)
        {
        }
    }
}
