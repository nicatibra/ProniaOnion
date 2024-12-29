namespace ProniaOnion.Application.DTOs.Products
{
    public record GetProductItemDto(int Id, string Name, decimal Price, string SKU, string Description);
}
