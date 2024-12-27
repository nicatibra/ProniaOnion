using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
            .HasMaxLength(100);

            builder
                .Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(8,2)");

            builder
                .Property(x => x.SKU)
                .HasColumnType("char(10)");
        }
    }
}
