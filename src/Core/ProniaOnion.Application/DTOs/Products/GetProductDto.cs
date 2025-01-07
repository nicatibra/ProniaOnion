using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Application.DTOs.Products
{
    public record GetProductDto(
        int Id,
        decimal Price,
        string Name,
        string SKU,
        string Description,
        GetCategoryItemDto Category,
        IEnumerable<GetColorItemDto> Colors,
        IEnumerable<GetSizeItemDto> Sizes,
        IEnumerable<GetTagItemDto> Tags
        );
}
