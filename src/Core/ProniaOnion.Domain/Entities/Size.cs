namespace ProniaOnion.Domain.Entities
{
    public class Size : BaseNameableEntity
    {
        //Relational Properties
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}
