using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Contexts;

namespace ProniaOnion.Persistence.Implementations.Repositories
{
    internal class SizeRepository : Repository<Size>, ISizeRepository
    {
        public SizeRepository(AppDbContext context) : base(context) { }
    }
}
