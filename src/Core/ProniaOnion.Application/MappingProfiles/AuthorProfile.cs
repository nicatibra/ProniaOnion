using AutoMapper;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, GetAuthorItemDto>();
            CreateMap<Author, GetAuthorDto>().ReverseMap();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>().ForMember(a => a.Id, opt => opt.Ignore());
        }
    }
}
