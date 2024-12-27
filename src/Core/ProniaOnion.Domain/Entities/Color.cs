namespace ProniaOnion.Domain.Entities
{
    public class Color : BaseNameableEntity
    {
        //Relational Properties
        public ICollection<ProductColor> ProductColors { get; set; }
    }
}
