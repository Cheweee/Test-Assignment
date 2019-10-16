
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.Services;
using Test.Utilities.Extensibility;

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScraperController : ControllerBase
    {
        private readonly ScraperService _scraper;

        public ScraperController(ScraperService scraper)
        {
            _scraper = scraper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ExtensibleGetOptions options)
        {
            return Ok(await _scraper.Process(options));
        }
    }
}