using AutoMapper;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class SizeProflie : Profile
    {
        public SizeProflie()
        {
            CreateMap<Size, GetSizeItemDto>();
            CreateMap<Size, GetSizeDto>().ReverseMap();
            CreateMap<CreateSizeDto, Size>();
            CreateMap<UpdateSizeDto, Size>().ForMember(s => s.Id, opt => opt.Ignore());
        }
    }
}
