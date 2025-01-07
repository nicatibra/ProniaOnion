using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Configurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar(30)");

            builder
                .Property(x => x.Surname)
                .IsRequired()
                .HasColumnType("nvarchar(30)");
        }
    }
}
