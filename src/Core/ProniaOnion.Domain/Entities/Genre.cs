namespace ProniaOnion.Domain.Entities
{
    public class Genre : BaseNameableEntity
    {
        public ICollection<Blog> Blogs { get; set; }
    }
}
