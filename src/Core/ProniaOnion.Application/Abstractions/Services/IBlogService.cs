using ProniaOnion.Application.DTOs.Blogs;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<GetBlogItemDto>> GetAllBlogsAsync(int page, int take);

        Task<GetBlogDto> GetBlogByIdAsync(int id);

        Task CreateBlogAsync(CreateBlogDto blogDto);

        Task UpdateBlogAsync(int id, UpdateBlogDto blogDto);

        Task DeleteBlogAsync(int id);
    }
}
