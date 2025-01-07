using AutoMapper;
using ProniaOnion.Application.DTOs.Genres;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GetGenreItemDto>();
            CreateMap<Genre, GetGenreDto>().ReverseMap();
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>().ForMember(g => g.Id, opt => opt.Ignore());
        }
    }
}
