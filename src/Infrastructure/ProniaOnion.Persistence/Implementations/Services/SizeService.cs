using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public SizeService(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }



        public async Task<IEnumerable<GetSizeItemDto>> GetAllSizesAsync(int page = 1, int take = 3)
        {
            IEnumerable<Size> sizesDTOs = await _sizeRepository
                .GetAll(skip: (page - 1) * take, take: take)
                .ToListAsync();

            #region WithoutMapper
            //IEnumerable<GetSizeItemDto> sizesDTOs = await _sizeRepository
            //    .GetAll(skip: (page - 1) * take, take: take)
            //    .Select(c => new GetSizeItemDto(c.Id, c.Name))
            //    .ToListAsync();
            //return sizesDTOs; 
            #endregion

            return _mapper.Map<IEnumerable<GetSizeItemDto>>(sizesDTOs);

        }


        public async Task<GetSizeDto> GetSizeByIdAsync(int id)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);

            if (size == null)
                throw new Exception("Not Found");

            //GetSizeDto getSizeDto = new(size.Id, size.Name);

            GetSizeDto getSizeDto = _mapper.Map<GetSizeDto>(size);

            return getSizeDto;
        }



        public async Task CreateSizeAsync(CreateSizeDto creaetSizeDto)
        {
            if (await _sizeRepository.AnyAsync(c => c.Name == creaetSizeDto.Name))
                throw new Exception("This size already exists");

            await _sizeRepository.AddAsync(new Size
            {
                Name = creaetSizeDto.Name
            });

            await _sizeRepository.SaveChangeAsync();
        }



        public async Task UpdateSizeAsync(int id, UpdateSizeDto updateSizeDto)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);
            if (size == null)
                throw new Exception("Not found");

            if (await _sizeRepository.AnyAsync(c => c.Name == updateSizeDto.Name && c.Id == id))
                throw new Exception("Already exists");



            size = _mapper.Map(updateSizeDto, size);

            //size.Name = updateSizeDto.Name;
            //size.Id = id;


            _sizeRepository.Update(size);
            await _sizeRepository.SaveChangeAsync();
        }



        public async Task DeleteSizeAsync(int id)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);

            if (size == null)
                throw new Exception("Not found");

            _sizeRepository.Delete(size);
            await _sizeRepository.SaveChangeAsync();
        }

        public async Task SoftDelete(int id)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);

            if (size == null)
                throw new Exception("Not found");

            size.IsDeleted = true;
            _sizeRepository.Update(size);

            await _sizeRepository.SaveChangeAsync();
        }
    }
}
