using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Colors;

namespace ProniaOnion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _service;
        private readonly IValidator<CreateColorDto> _validator;

        public ColorsController(IColorService service, IValidator<CreateColorDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllColorsAsync(page, take));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var colorDetailDTO = await _service.GetColorByIdAsync(id);

            if (colorDetailDTO == null)
                return NotFound();
            return StatusCode(StatusCodes.Status200OK, colorDetailDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateColorDto createColorDto)
        {
            //ValidationResult result = await _validator.ValidateAsync(createColorDto);
            //if (!result.IsValid)
            //{
            //    foreach (ValidationFailure error in result.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }

            //    return BadRequest(ModelState);
            //}

            await _service.CreateColorAsync(createColorDto);

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateColorDto updateColorDto)
        {
            if (id < 1)
                return BadRequest();

            await _service.UpdateColorAsync(id, updateColorDto);

            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _service.DeleteColorAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
