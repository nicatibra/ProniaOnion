namespace ProniaOnion.Application.DTOs.Products
{
    public record UpdateProductDto(
        string Name,
        decimal Price,
        string SKU,
        string Description,
        int CategoryId,
        ICollection<int> ColorIds,
        ICollection<int> SizeIds,
        ICollection<int> TagIds
        );
}
