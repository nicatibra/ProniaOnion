using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(user => user.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.Property(user => user.Surname)
                .IsRequired()
                .HasColumnType("nvarchar(50)");
        }
    }
}
