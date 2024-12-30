using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Sizes;

namespace ProniaOnion.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _service;

        public SizesController(ISizeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllSizesAsync(page, take));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var sizeDetailDTO = await _service.GetSizeByIdAsync(id);

            if (sizeDetailDTO == null)
                return NotFound();
            return StatusCode(StatusCodes.Status200OK, sizeDetailDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSizeDto createSizeDto)
        {
            await _service.CreateSizeAsync(createSizeDto);

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSizeDto updateSizeDto)
        {
            if (id < 1)
                return BadRequest();

            await _service.UpdateSizeAsync(id, updateSizeDto);

            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _service.DeleteSizeAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
