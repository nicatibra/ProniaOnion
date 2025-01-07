using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetCategoryItemDto>> GetAllCategoriesAsync(int page, int take)
        {
            IEnumerable<Category> categoriesDTOs = await _categoryRepository
                .GetAll(skip: (page - 1) * take, take: take, ignoreQueryFilters: true)
                .ToListAsync();

            #region WithoutMapper
            //IEnumerable<GetCategoryItemDto> categoriesDTOs = await _categoryRepository
            //    .GetAll(skip: (page - 1) * take, take: take)
            //    .Select(c => new GetCategoryItemDto(c.Id, c.Name))
            //    .ToListAsync();
            //return categoriesDTOs; 
            #endregion

            return _mapper.Map<IEnumerable<GetCategoryItemDto>>(categoriesDTOs);
        }

        public async Task<GetCategoryDto> GetCategoryByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id, nameof(Category.Products));

            if (category == null)
                throw new Exception("Not found");


            GetCategoryDto getCategoryDto = _mapper.Map<GetCategoryDto>(category);

            #region WithoutMapping
            //GetCategoryDto getCategoryDto = new(category.Id, category.Name,
            //    category.Products.Select(p => new GetProductItemDto(p.Id, p.Name, p.Price, p.SKU, p.Description)).ToList()
            //    ); 
            #endregion

            return getCategoryDto;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (await _categoryRepository.AnyAsync(c => c.Name == createCategoryDto.Name))
                throw new Exception("This Category already exists");


            var category = _mapper.Map<Category>(createCategoryDto);

            await _categoryRepository.AddAsync(category);

            await _categoryRepository.SaveChangeAsync();
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Not found");

            if (await _categoryRepository.AnyAsync(c => c.Name == updateCategoryDto.Name && c.Id == id))
                throw new Exception("Already exists");


            //category.Name = updateCategoryDto.Name;

            category = _mapper.Map(updateCategoryDto, category);


            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangeAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new Exception("Not found");

            _categoryRepository.Delete(category);

            await _categoryRepository.SaveChangeAsync();
        }

        public async Task SoftDelete(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new Exception("Not found");

            category.IsDeleted = true;
            _categoryRepository.Update(category);

            await _categoryRepository.SaveChangeAsync();
        }
    }
}
