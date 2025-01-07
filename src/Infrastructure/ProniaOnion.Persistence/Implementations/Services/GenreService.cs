using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Genres;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        public async Task<IEnumerable<GetGenreItemDto>> GetAllGenresAsync(int page, int take)
        {
            IEnumerable<Genre> genres = await _repository
                .GetAll(skip: (page - 1) * take, take: take)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetGenreItemDto>>(genres);
        }

        public async Task<GetGenreDto> GetGenreByIdAsync(int id)
        {
            Genre genre = await _repository.GetByIdAsync(id, nameof(Genre.Blogs));

            if (genre is null)
                throw new Exception(" Genre does not exist");

            return _mapper.Map<GetGenreDto>(genre);
        }

        public async Task CreateGenreAsync(CreateGenreDto genreDto)
        {
            if (await _repository.AnyAsync(c => c.Name == genreDto.Name))
                throw new Exception("This Genre already exists");

            var genre = _mapper.Map<Genre>(genreDto);

            await _repository.AddAsync(genre);

            await _repository.SaveChangeAsync();
        }

        public async Task UpdateGenreAsync(int id, UpdateGenreDto genreDto)
        {
            Genre genre = await _repository.GetByIdAsync(id);

            if (genre is null)
                throw new Exception("Not Found");

            if (await _repository.AnyAsync(c => c.Name == genreDto.Name && c.Id != id))
                throw new Exception("This genre already exists");

            _mapper.Map(genreDto, genre);

            _repository.Update(genre);

            await _repository.SaveChangeAsync();
        }


        public async Task DeleteGenreAsync(int id)
        {
            Genre genre = await _repository.GetByIdAsync(id);

            if (genre is null)
                throw new Exception("Not Found");

            _repository.Delete(genre);

            await _repository.SaveChangeAsync();
        }

        public async Task SoftDelete(int id)
        {
            Genre genre = await _repository.GetByIdAsync(id);

            if (genre == null)
                throw new Exception("Not found");

            genre.IsDeleted = true;
            _repository.Update(genre);

            await _repository.SaveChangeAsync();
        }
    }

}
