using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Configurations
{
    internal class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder
                .Property(x => x.Name)
                .HasColumnType("varchar(100)");

            builder
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
