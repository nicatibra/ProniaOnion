using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Contexts;

namespace ProniaOnion.Persistence.Implementations.Repositories
{
    internal class ColorRepository : Repository<Color>, IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context) { }

    }
}
