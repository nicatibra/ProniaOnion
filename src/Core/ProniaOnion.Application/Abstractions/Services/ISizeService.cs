using ProniaOnion.Application.DTOs.Sizes;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ISizeService
    {
        Task<IEnumerable<GetSizeItemDto>> GetAllSizesAsync(int page, int take);

        Task<GetSizeDto> GetSizeByIdAsync(int id);

        Task CreateSizeAsync(CreateSizeDto createSizeDto);

        Task UpdateSizeAsync(int id, UpdateSizeDto updateSizeDto);

        Task DeleteSizeAsync(int id);
    }
}
