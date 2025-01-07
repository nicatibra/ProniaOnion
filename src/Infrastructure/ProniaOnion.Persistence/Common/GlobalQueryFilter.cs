using Microsoft.EntityFrameworkCore;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyFilter<TEntity>(this ModelBuilder modelBuilder) where TEntity : BaseEntity, new()
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
        }

        public static void ApplyQueryFilters(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyFilter<Product>();
            modelBuilder.ApplyFilter<Category>();
            modelBuilder.ApplyFilter<Color>();
            modelBuilder.ApplyFilter<Tag>();
            modelBuilder.ApplyFilter<Size>();

            modelBuilder.ApplyFilter<Author>();
            modelBuilder.ApplyFilter<Blog>();
            modelBuilder.ApplyFilter<Genre>();
        }
    }
}
