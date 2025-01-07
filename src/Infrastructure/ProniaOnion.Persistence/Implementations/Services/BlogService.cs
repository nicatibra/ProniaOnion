using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Domain.Entities;


namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetBlogItemDto>> GetAllBlogsAsync(int page, int take)
        {
            IEnumerable<Blog> blogs = await _repository
                .GetAll(skip: (page - 1) * take, take: take, includes: nameof(Blog.Author))
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetBlogItemDto>>(blogs);
        }

        public async Task<GetBlogDto> GetBlogByIdAsync(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id, nameof(Blog.Author), nameof(Blog.Genre));

            if (blog == null)
                throw new Exception("Not Found");

            return _mapper.Map<GetBlogDto>(blog);
        }

        public async Task CreateBlogAsync(CreateBlogDto blogdto)
        {

            var blog = _mapper.Map<Blog>(blogdto);

            await _repository.AddAsync(blog);

            await _repository.SaveChangeAsync();
        }

        public async Task UpdateBlogAsync(int Id, UpdateBlogDto blogdto)
        {
            Blog blog = await _repository.GetByIdAsync(Id);

            if (blog == null)
                throw new Exception("Not Found");


            _mapper.Map(blogdto, blog);

            _repository.Update(blog);

            await _repository.SaveChangeAsync();
        }

        public async Task DeleteBlogAsync(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);

            if (blog == null)
                throw new Exception("Not Found");

            _repository.Delete(blog);

            await _repository.SaveChangeAsync();
        }

        public async Task SoftDelete(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);

            if (blog == null)
                throw new Exception("Not found");

            blog.IsDeleted = true;
            _repository.Update(blog);

            await _repository.SaveChangeAsync();
        }
    }

}
