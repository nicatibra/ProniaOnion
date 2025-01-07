using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetProductItemDto>> GetAllProductsAsync(int page, int take)
        {
            var products = _mapper
                .Map<IEnumerable<GetProductItemDto>>(
                    await _productRepository
                    .GetAll(skip: (page - 1) * take, take: take)
                    .ToListAsync()
                    );

            return products;
        }

        public async Task<GetProductDto> GetProductByIdAsync(int id)
        {
            var product = _mapper.Map<GetProductDto>(await _productRepository.GetByIdAsync(id, "Category", "ProductColors.Color", "ProductSizes.Size", "ProductTags.Tag"));

            if (product == null)
                throw new Exception("Product not found");

            return product;
        }

    }
}
