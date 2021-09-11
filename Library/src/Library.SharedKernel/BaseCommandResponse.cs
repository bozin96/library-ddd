using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.SharedKernel
{
    public class BaseCommandResponse
    {
        public bool Status { get; set; } = true;
        public int StatusCode { get; set; } = 200;
        public List<string> Errors { get; set; } = new List<string>();
        public object Data { get; set; }

        public BaseCommandResponse()
        {

        }

        public void AddError(string error, int statusCode = 400)
        {
            Status = false;
            StatusCode = statusCode;
            Errors.Add(error);
        }

    }
}
