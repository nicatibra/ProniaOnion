namespace ProniaOnion.Domain.Entities
{
    public class Tag : BaseNameableEntity
    {
        //Relational Properties
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
