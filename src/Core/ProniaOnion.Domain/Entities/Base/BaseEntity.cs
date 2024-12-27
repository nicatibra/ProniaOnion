namespace ProniaOnion.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        protected BaseEntity()
        {
            CreatedBy = "Admin";
        }
    }
}
