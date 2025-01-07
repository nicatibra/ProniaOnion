using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaOnion.Domain.Entities;


namespace ProniaOnion.Persistence.Configurations
{
    internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder
                .Property(b => b.Article)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            builder
                .Property(b => b.Title)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
        }
    }
}
