using System.Collections.Generic;

namespace Library.Application.Dtos.Patrons
{
    public class PatronAddResponse : BaseResponse
    {
        public PatronAddResponse()
        {

        }

        public PatronAddResponse(bool status, int statusCode, List<string> errors, object data) : base(status, statusCode, errors, data)
        {
        }
    }
}
