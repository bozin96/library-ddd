using Library.Application.Dtos.Patrons;
using Library.Application.Interfaces;
using Library.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    [Route("api/patrons")]
    [ApiController]
    public class PatronsController : ControllerBase
    {
        private readonly IPatronService _patronService;

        public PatronsController(
            IPatronService patronService)
        {
            _patronService = patronService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatrons([FromQuery] ResourceParameters resourceParameters)
        {
            var response = await _patronService.GetPatrons(resourceParameters);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddPatron(PatronAddRequest request)
        {
            var response = _patronService.AddPatron(request);
            return Ok(response);
        }
    }
}
