using AutoMapper;
using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, GetBlogItemDto>();
            CreateMap<Blog, GetBlogDto>().ReverseMap();
            CreateMap<CreateBlogDto, Blog>();
            CreateMap<UpdateBlogDto, Blog>().ForMember(b => b.Id, opt => opt.Ignore());
        }
    }
}
