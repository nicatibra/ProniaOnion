using AutoMapper;
using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, GetBlogItemDto>()
                .ForCtorParam(nameof(GetProductDto.Tags), opt => opt.MapFrom(
                    p => p.BlogTags.Select(pt => new GetTagItemDto(pt.TagId, pt.Tag.Name)).ToList())
                );

            CreateMap<Blog, GetBlogDto>().ReverseMap();
            CreateMap<CreateBlogDto, Blog>();
            CreateMap<UpdateBlogDto, Blog>().ForMember(b => b.Id, opt => opt.Ignore());
        }
    }
}
