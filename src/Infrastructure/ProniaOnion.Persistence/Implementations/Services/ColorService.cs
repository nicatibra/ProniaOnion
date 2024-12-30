using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }



        public async Task<IEnumerable<GetColorItemDto>> GetAllColorsAsync(int page = 1, int take = 3)
        {
            IEnumerable<Color> colorsDTOs = await _colorRepository
                .GetAll(skip: (page - 1) * take, take: take)
                .ToListAsync();

            #region WithoutMapper
            //IEnumerable<GetColorItemDto> colorsDTOs = await _colorRepository
            //    .GetAll(skip: (page - 1) * take, take: take)
            //    .Select(c => new GetColorItemDto(c.Id, c.Name))
            //    .ToListAsync();
            //return colorsDTOs; 
            #endregion

            return _mapper.Map<IEnumerable<GetColorItemDto>>(colorsDTOs);

        }


        public async Task<GetColorDto> GetColorByIdAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);

            if (color == null)
                throw new Exception("Not Found");

            //GetColorDto getColorDto = new(color.Id, color.Name);

            GetColorDto getColorDto = _mapper.Map<GetColorDto>(color);

            return getColorDto;
        }



        public async Task CreateColorAsync(CreateColorDto creaetColorDto)
        {
            if (await _colorRepository.AnyAsync(c => c.Name == creaetColorDto.Name))
                throw new Exception("This color already exists");

            await _colorRepository.AddAsync(new Color
            {
                Name = creaetColorDto.Name,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            });

            await _colorRepository.SaveChangeAsync();
        }



        public async Task UpdateColorAsync(int id, UpdateColorDto updateColorDto)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null)
                throw new Exception("Not found");

            if (await _colorRepository.AnyAsync(c => c.Name == updateColorDto.Name && c.Id == id))
                throw new Exception("Already exists");



            color = _mapper.Map(updateColorDto, color);

            //color.Name = updateColorDto.Name;
            //color.Id = id;
            color.ModifiedAt = DateTime.Now;


            _colorRepository.Update(color);
            await _colorRepository.SaveChangeAsync();
        }



        public async Task DeleteColorAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);

            if (color == null)
                throw new Exception("Not found");

            _colorRepository.Delete(color);
            await _colorRepository.SaveChangeAsync();
        }
    }
}
