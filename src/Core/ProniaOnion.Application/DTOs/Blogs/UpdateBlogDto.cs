namespace ProniaOnion.Application.DTOs.Blogs
{
    public record UpdateBlogDto(string Title, string Article, string Image, int AuthorId, int GenreId, ICollection<int> TagIds);
}
