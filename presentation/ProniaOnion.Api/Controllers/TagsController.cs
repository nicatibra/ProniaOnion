using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllTagsAsync(page, take));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var tagDetailDTO = await _service.GetTagByIdAsync(id);

            if (tagDetailDTO == null)
                return NotFound();
            return StatusCode(StatusCodes.Status200OK, tagDetailDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTagDto createTagDto)
        {
            await _service.CreateTagAsync(createTagDto);

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTagDto updateTagDto)
        {
            if (id < 1)
                return BadRequest();

            await _service.UpdateTagAsync(id, updateTagDto);

            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _service.DeleteTagAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
