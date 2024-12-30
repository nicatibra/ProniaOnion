using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<IEnumerable<GetTagItemDto>> GetAllTagsAsync(int page, int take);

        Task<GetTagDto> GetTagByIdAsync(int id);

        Task CreateTagAsync(CreateTagDto createTagDto);

        Task UpdateTagAsync(int id, UpdateTagDto updateTagDto);

        Task DeleteTagAsync(int id);
    }
}
