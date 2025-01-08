using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductItemDto>> GetAllProductsAsync(int page, int take);

        Task<GetProductDto> GetProductByIdAsync(int id);

        Task CreateProductAsync(CreateProductDto productDto);

        Task UpdateProductAsync(int id, UpdateProductDto productDto);
    }
}
