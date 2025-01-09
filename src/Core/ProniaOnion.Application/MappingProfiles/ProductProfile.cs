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


            CreateMap<CreateProductDto, Product>()
                .ForMember(p => p.ProductColors, opt => opt.MapFrom(
                    pDto => pDto.ColorIds.Select(ci => new ProductColor { ColorId = ci }).ToList())
                )
                .ForMember(p => p.ProductSizes, opt => opt.MapFrom(
                    pDto => pDto.SizeIds.Select(si => new ProductSize { SizeId = si }).ToList())
                )
                .ForMember(p => p.ProductTags, opt => opt.MapFrom(
                    pDto => pDto.TagIds.Select(ti => new ProductTag { TagId = ti }).ToList())
                );

            CreateMap<UpdateProductDto, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore()
                )
                .ForMember(p => p.ProductColors, opt => opt.MapFrom(
                    pDto => pDto.ColorIds.Select(ci => new ProductColor { ColorId = ci }).ToList())
                )
                .ForMember(p => p.ProductSizes, opt => opt.MapFrom(
                    pDto => pDto.SizeIds.Select(si => new ProductSize { SizeId = si }).ToList())
                )
                .ForMember(p => p.ProductTags, opt => opt.MapFrom(
                    pDto => pDto.TagIds.Select(ti => new ProductTag { TagId = ti }).ToList())
                );
        }
    }
}
