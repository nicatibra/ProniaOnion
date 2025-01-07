namespace ProniaOnion.Domain.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Article { get; set; }
        public string Image { get; set; }


        //Relational properties
        public int AuthorId { get; set; }
        public Author Author { get; set; }


        public int GenreId { get; set; }
        public Genre Genre { get; set; }


        public ICollection<BlogTag> BlogTags { get; set; }
    }
}
