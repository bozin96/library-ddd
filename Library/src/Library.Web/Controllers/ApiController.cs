using Library.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Library.Web.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        public ApiController()
        {

        }

        protected new IActionResult Response(BaseResponse result = null)
        {
            if (result.Status)
            {
                return Ok(new
                {
                    success = true,
                    data = result.Data
                });
            }

            return StatusCode(result.StatusCode, new
            {
                success = false,
                errors = result.Errors
            });
        }

        protected IActionResult ResponseModelStateErrors()
        {
            List<string> errors = new List<string>();
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                errors.Add(errorMsg);
            }
            return StatusCode(StatusCodes.Status422UnprocessableEntity, new
            {
                sueccess = false,
                errors = errors
            });
        }
    }
}
