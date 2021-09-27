using Library.Application.Dtos.Authors;
using Library.Application.Interfaces;
using Library.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(
            IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors([FromQuery] ResourceParameters resourceParameters)
        {
            var response = await _authorService.GetAuthors(resourceParameters);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorAddRequest request)
        {
            var response = _authorService.AddAuthor(request);
            return Ok(response);
        }
    }
}
