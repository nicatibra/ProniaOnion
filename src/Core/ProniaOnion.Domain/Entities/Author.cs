namespace ProniaOnion.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfileImage { get; set; }

        //Relational properties
        public ICollection<Blog> Blogs { get; set; }
    }
}
