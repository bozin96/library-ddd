using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel
{
    public class DomainActionStatus
    {
        public bool Status { get; set; } = true;

        public string Error { get; set; }

        public int StatusCode { get; set; } = 200;

        public DomainActionStatus()
        {

        }

        public DomainActionStatus(string error)
        {
            Status = false;
            Error = error;
            StatusCode = 400;
        }

        public DomainActionStatus(string error, int statusCode)
        {
            Status = false;
            Error = error;
            StatusCode = statusCode;
        }

        public void AddError(string error, int statusCode = 400)
        {
            Status = false;
            Error = error;
            StatusCode = statusCode;
        }
    }
}
