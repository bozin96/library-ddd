using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Application.Dtos
{
    /// <summary>
    /// Base class used by API responses
    /// </summary>
    public abstract class BaseResponse : BaseMessage
    {
        public bool Status { get; set; } = true;
        public int StatusCode { get; set; } = 200;
        public List<string> Errors { get; set; } = new List<string>();
        public virtual object Data { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(Guid correlationId) : base()
        {
            base._correlationId = correlationId;
        }

        public BaseResponse(bool status, int statusCode, List<string> errors, object data)
        {
            Status = status;
            StatusCode = statusCode;
            Errors = errors;
            Data = data;
        }
    }
}
