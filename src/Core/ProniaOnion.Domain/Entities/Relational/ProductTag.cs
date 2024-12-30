namespace ProniaOnion.Domain.Entities
{
    public class ProductTag
    {
        public int ProductId { get; set; }

        public int TagId { get; set; }


        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}
