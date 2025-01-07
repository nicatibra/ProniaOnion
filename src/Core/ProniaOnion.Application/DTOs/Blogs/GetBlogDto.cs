using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Genres;

namespace ProniaOnion.Application.DTOs.Blogs
{
    public record GetBlogDto(int Id, string Title, string Article, string Image, GetAuthorDto Author, GetGenreDto Genre);
}
