using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Data.Models;
using Test.Services;

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _service;

        public InstructorController(InstructorService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]InstructorGetOptions options)
        {
            return Ok(await _service.Get(options));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]Instructor instructor)
        {
            return Ok(await _service.Create(instructor));
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Put([FromBody]Instructor instructor)
        {
            return Ok(await _service.Update(instructor));
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}