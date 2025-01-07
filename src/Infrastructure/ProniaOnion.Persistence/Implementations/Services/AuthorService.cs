using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAuthorItemDto>> GetAllAuthorsAsync(int page, int take)
        {
            IEnumerable<Author> authors = await _repository
                .GetAll(skip: (page - 1) * take, take: take)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetAuthorItemDto>>(authors);
        }

        public async Task<GetAuthorDto> GetAuthorByIdAsync(int id)
        {
            Author author = await _repository.GetByIdAsync(id, nameof(Author.Blogs));

            if (author == null)
                throw new Exception("Not Found");


            return _mapper.Map<GetAuthorDto>(author);
        }

        public async Task CreateAuthorAsync(CreateAuthorDto authordto)
        {
            if (await _repository.AnyAsync(c => c.Name == authordto.Name))
                throw new Exception("This Category already exists");

            var author = _mapper.Map<Author>(authordto);

            await _repository.AddAsync(author);

            await _repository.SaveChangeAsync();
        }

        public async Task UpdateAuthorAsync(int id, UpdateAuthorDto authordto)
        {
            Author author = await _repository.GetByIdAsync(id);

            if (author == null)
                throw new Exception("Not Found");

            if (await _repository.AnyAsync(c => c.Name == authordto.Name && c.Id == id))
                throw new Exception("Already exists");

            _mapper.Map(authordto, author);

            _repository.Update(author);

            await _repository.SaveChangeAsync();
        }

        public async Task DeleteAuthorAsync(int Id)
        {
            Author author = await _repository.GetByIdAsync(Id);

            if (author == null)
                throw new Exception("Not Found");

            _repository.Delete(author);

            await _repository.SaveChangeAsync();
        }

        public async Task SoftDelete(int id)
        {
            Author author = await _repository.GetByIdAsync(id);

            if (author == null)
                throw new Exception("Not found");

            author.IsDeleted = true;
            _repository.Update(author);

            await _repository.SaveChangeAsync();
        }
    }
}
