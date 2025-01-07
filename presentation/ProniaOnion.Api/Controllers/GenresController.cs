using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Genres;

namespace ProniaOnion.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllGenresAsync(page, take));
        }




        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            if (Id < 1) return BadRequest();
            var genredto = await _service.GetGenreByIdAsync(Id);
            if (genredto == null) return NotFound();
            return Ok(genredto);
        }




        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateGenreDto genredto)
        {
            await _service.CreateGenreAsync(genredto);

            return StatusCode(StatusCodes.Status201Created);
        }




        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm] UpdateGenreDto genredto)
        {
            if (Id < 1) return BadRequest();
            await _service.UpdateGenreAsync(Id, genredto);
            return NoContent();
        }




        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id < 1) return BadRequest();
            await _service.DeleteGenreAsync(Id);
            return NoContent();
        }
    }

}
