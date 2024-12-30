using AutoMapper;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, GetTagItemDto>();
            CreateMap<Tag, GetTagDto>().ReverseMap();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>().ForMember(t => t.Id, opt => opt.Ignore());
        }
    }
}
