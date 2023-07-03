namespace FastFood.Application.Features.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public string ImagePath { get; set; } = null!;
    public int CategoryId { get; set; }
}