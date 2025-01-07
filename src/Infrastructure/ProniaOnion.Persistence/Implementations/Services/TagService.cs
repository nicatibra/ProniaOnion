using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }



        public async Task<IEnumerable<GetTagItemDto>> GetAllTagsAsync(int page = 1, int take = 3)
        {
            IEnumerable<Tag> tagsDTOs = await _tagRepository
                .GetAll(skip: (page - 1) * take, take: take)
                .ToListAsync();

            #region WithoutMapper
            //IEnumerable<GetTagItemDto> tagsDTOs = await _tagRepository
            //    .GetAll(skip: (page - 1) * take, take: take)
            //    .Select(c => new GetTagItemDto(c.Id, c.Name))
            //    .ToListAsync();
            //return tagsDTOs; 
            #endregion

            return _mapper.Map<IEnumerable<GetTagItemDto>>(tagsDTOs);

        }


        public async Task<GetTagDto> GetTagByIdAsync(int id)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null)
                throw new Exception("Not Found");

            //GetTagDto getTagDto = new(tag.Id, tag.Name);

            GetTagDto getTagDto = _mapper.Map<GetTagDto>(tag);

            return getTagDto;
        }



        public async Task CreateTagAsync(CreateTagDto creaetTagDto)
        {
            if (await _tagRepository.AnyAsync(c => c.Name == creaetTagDto.Name))
                throw new Exception("This tag already exists");

            await _tagRepository.AddAsync(new Tag
            {
                Name = creaetTagDto.Name
            });

            await _tagRepository.SaveChangeAsync();
        }



        public async Task UpdateTagAsync(int id, UpdateTagDto updateTagDto)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null)
                throw new Exception("Not found");

            if (await _tagRepository.AnyAsync(c => c.Name == updateTagDto.Name && c.Id == id))
                throw new Exception("Already exists");



            tag = _mapper.Map(updateTagDto, tag);

            //tag.Name = updateTagDto.Name;
            //tag.Id = id;


            _tagRepository.Update(tag);
            await _tagRepository.SaveChangeAsync();
        }



        public async Task DeleteTagAsync(int id)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null)
                throw new Exception("Not found");

            _tagRepository.Delete(tag);
            await _tagRepository.SaveChangeAsync();
        }

        public async Task SoftDelete(int id)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null)
                throw new Exception("Not found");

            tag.IsDeleted = true;
            _tagRepository.Update(tag);

            await _tagRepository.SaveChangeAsync();
        }
    }
}
