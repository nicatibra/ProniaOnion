using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IValidator<CreateCategoryDto> _validator;

        public CategoriesController(ICategoryService service, IValidator<CreateCategoryDto> validator)
        {
            _service = service;
            _validator = validator;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllCategoriesAsync(page, take));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var categoryDetailDto = await _service.GetCategoryByIdAsync(id);

            if (categoryDetailDto == null)
                return NotFound();

            return StatusCode(StatusCodes.Status200OK, categoryDetailDto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto createCategoryDto)
        {
            //ValidationResult result = await _validator.ValidateAsync(createCategoryDto);
            //if (!result.IsValid)
            //{
            //    foreach (ValidationFailure error in result.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }

            //    return BadRequest(ModelState);
            //}


            await _service.CreateCategoryAsync(createCategoryDto);

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (id < 1)
                return BadRequest();

            await _service.UpdateCategoryAsync(id, updateCategoryDto);

            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _service.DeleteCategoryAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }


    }
}
