using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Authors;

namespace ProniaOnion.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllAuthorsAsync(page, take));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var authordto = await _service.GetAuthorByIdAsync(id);

            if (authordto == null)
                return NotFound();

            return Ok(authordto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAuthorDto authordto)
        {
            await _service.CreateAuthorAsync(authordto);

            return Created();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAuthorDto authordto)
        {
            if (id < 1)
                return BadRequest();

            await _service.UpdateAuthorAsync(id, authordto);

            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _service.DeleteAuthorAsync(id);

            return NoContent();
        }
    }

}
