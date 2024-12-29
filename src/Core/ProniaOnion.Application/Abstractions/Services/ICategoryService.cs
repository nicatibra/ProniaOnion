using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryItemDto>> GetAllCategoriesAsync(int page, int take);

        Task<GetCategoryDto> GetCategoryByIdAsync(int id);

        Task CreateCategoryAsync(CreateCategoryDto categoryDto);

        Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);

        Task DeleteCategoryAsync(int id);
    }
}
