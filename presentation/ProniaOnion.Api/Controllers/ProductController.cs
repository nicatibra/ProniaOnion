using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;

namespace ProniaOnion.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service
        }


    }
}
