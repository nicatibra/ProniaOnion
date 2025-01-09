using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Application.DTOs.Blogs
{
    public record GetBlogItemDto(int Id, string Title, string Article, string Image, GetAuthorInBlogDto Author, IEnumerable<GetTagItemDto> Tags);
}
