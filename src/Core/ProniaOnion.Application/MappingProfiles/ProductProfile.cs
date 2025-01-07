using AutoMapper;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductItemDto>().ReverseMap();

            CreateMap<Product, GetProductDto>()
                .ForCtorParam(nameof(GetProductDto.Colors), opt => opt.MapFrom(
                    p => p.ProductColors.Select(pc => new GetColorItemDto(pc.ColorId, pc.Color.Name)).ToList())
                )

                .ForCtorParam(nameof(GetProductDto.Sizes), opt => opt.MapFrom(
                    p => p.ProductSizes.Select(ps => new GetSizeItemDto(ps.SizeId, ps.Size.Name)).ToList())
                )

                .ForCtorParam(nameof(GetProductDto.Tags), opt => opt.MapFrom(
                    p => p.ProductTags.Select(pt => new GetTagItemDto(pt.TagId, pt.Tag.Name)).ToList())
                );
        }
    }
}
