using Library.Application.Interfaces;
using Library.Application.Dtos.BookLendings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.SharedKernel;

namespace Library.Web.Controllers
{
    [Route("api/library/{libraryId}/books")]
    public class BooksController : ApiController
    {
        private readonly ILibraryService _libraryService;

        public BooksController(
            ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(Guid libraryId, [FromQuery] ResourceParameters resourceParameters)
        {
            var response = await _libraryService.GetLibraryBooks(libraryId, resourceParameters);
            return Ok(response);
        }



        [HttpPost("lend")]
        public IActionResult LendBooks(Guid libraryId, [FromBody] BookLendingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ResponseModelStateErrors();
            }

            if (libraryId != request.LibraryId)
                return BadRequest(new { message = "Wrong library id" });

            var response = _libraryService.LendBooks(request);
            return Response(response);
        }

        [HttpPost("return")]
        public IActionResult ReturnBooks(Guid libraryId, [FromBody] BookReturningRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ResponseModelStateErrors();
            }

            if (libraryId != request.LibraryId)
                return BadRequest(new { message = "Wrong library id" });

            var response = _libraryService.ReturnBooks(request);
            return Response(response);
        }
    }
}
