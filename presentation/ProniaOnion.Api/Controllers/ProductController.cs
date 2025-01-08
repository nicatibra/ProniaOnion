using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 8)
        {
            return Ok(await _service.GetAllProductsAsync(page, take));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest("Invalid id");

            return Ok(await _service.GetProductByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateProductDto productDto)
        {
            await _service.CreateProductAsync(productDto);
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductDto updateProductDto)
        {
            if (id < 1) return BadRequest("Invalid id");
            await _service.UpdateProductAsync(id, updateProductDto);
            return NoContent();
        }
    }
}
