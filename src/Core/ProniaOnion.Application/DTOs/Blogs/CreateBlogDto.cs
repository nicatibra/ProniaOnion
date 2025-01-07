namespace ProniaOnion.Application.DTOs.Blogs
{
    public record CreateBlogDto(string Title, string Article, string Image, int AuthorId, int GenreId);

}
