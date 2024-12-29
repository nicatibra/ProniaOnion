using ProniaOnion.Application.DTOs.Colors;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<IEnumerable<GetColorItemDto>> GetAllColorsAsync(int page, int take);

        Task<GetColorDto> GetColorByIdAsync(int id);

        Task CreateColorAsync(CreateColorDto createColorDto);

        Task UpdateColorAsync(int id, UpdateColorDto updateColorDto);

        Task DeleteColorAsync(int id);
    }
}
