using ProniaOnion.Application.DTOs.Authors;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<GetAuthorItemDto>> GetAllAuthorsAsync(int page, int take);

        Task<GetAuthorDto> GetAuthorByIdAsync(int id);

        Task CreateAuthorAsync(CreateAuthorDto authorDto);

        Task UpdateAuthorAsync(int id, UpdateAuthorDto authorDto);

        Task DeleteAuthorAsync(int id);
    }
}
