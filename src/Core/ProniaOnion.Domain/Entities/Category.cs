namespace ProniaOnion.Domain.Entities
{
    public class Category : BaseNameableEntity
    {
        //Relational Properties
        public ICollection<Product> Products { get; set; }
    }
}
