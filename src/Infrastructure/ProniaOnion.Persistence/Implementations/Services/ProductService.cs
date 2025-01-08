using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IColorRepository colorRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
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

        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new Exception($"Category not found with that id");


            var colorEntities = await _productRepository.GetManyToManyEntities<Color>(productDto.ColorIds);
            if (colorEntities.Count() != productDto.ColorIds.Distinct().Count())
                throw new Exception($"Some colors not found with that id");


            var sizeEntities = await _productRepository.GetManyToManyEntities<Size>(productDto.SizeIds);
            if (sizeEntities.Count() != productDto.SizeIds.Distinct().Count())
                throw new Exception($"Some sizes not found with that id");


            var tagEntities = await _productRepository.GetManyToManyEntities<Tag>(productDto.TagIds);
            if (tagEntities.Count() != productDto.TagIds.Distinct().Count())
                throw new Exception($"Some tags not found with that id");


            await _productRepository.AddAsync(_mapper.Map<Product>(productDto));

            await _productRepository.SaveChangeAsync();
        }

        public async Task UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            Product product = await _productRepository.GetByIdAsync(id, "ProductColors", "ProductSizes", "ProductTags");

            if (updateProductDto.CategoryId != product.CategoryId)
            {
                if (!await _categoryRepository.AnyAsync(c => c.Id == updateProductDto.CategoryId))
                    throw new Exception($"Category not found with that id");
            }

            //Without Mapper
            //product.ProductColors = product.ProductColors.Where(pc => updateProductDto.ColorIds.Contains(pc.ColorId)).ToList();


            ICollection<int> createColorItem = updateProductDto.ColorIds.Where(cId => !product.ProductColors.Any(pc => pc.ColorId == cId)).ToList();
            var colorEntities = await _productRepository.GetManyToManyEntities<Color>(createColorItem);
            if (colorEntities.Count() != createColorItem.Distinct().Count())
                throw new Exception($"Some colors not found with that id");

            ICollection<int> createSizeItem = updateProductDto.SizeIds.Where(sId => !product.ProductSizes.Any(ps => ps.SizeId == sId)).ToList();
            var sizeEntities = await _productRepository.GetManyToManyEntities<Size>(createSizeItem);
            if (sizeEntities.Count() != createSizeItem.Distinct().Count())
                throw new Exception($"Some sizes not found with that id");

            ICollection<int> createTagItem = updateProductDto.TagIds.Where(tId => !product.ProductTags.Any(pt => pt.TagId == tId)).ToList();
            var tagEntities = await _productRepository.GetManyToManyEntities<Tag>(createTagItem);
            if (tagEntities.Count() != createTagItem.Distinct().Count())
                throw new Exception($"Some tags not found with that id");

            _productRepository.Update(_mapper.Map(updateProductDto, product));
            await _productRepository.SaveChangeAsync();
        }
    }
}
