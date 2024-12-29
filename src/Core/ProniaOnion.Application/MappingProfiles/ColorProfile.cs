using AutoMapper;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, GetColorItemDto>();
            CreateMap<Color, GetColorDto>().ReverseMap();
            CreateMap<CreateColorDto, Color>();
            CreateMap<UpdateColorDto, Color>();
        }
    }
}
