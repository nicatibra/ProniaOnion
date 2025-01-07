using ProniaOnion.Application.DTOs.Genres;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GetGenreItemDto>> GetAllGenresAsync(int page, int take);

        Task<GetGenreDto> GetGenreByIdAsync(int id);

        Task CreateGenreAsync(CreateGenreDto genreDto);

        Task UpdateGenreAsync(int id, UpdateGenreDto genreDto);

        Task DeleteGenreAsync(int id);
    }
}
