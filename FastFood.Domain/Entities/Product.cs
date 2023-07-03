using FastFood.Domain.Entities.Base;

namespace FastFood.Domain.Entities;

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public string ImagePath { get; set; } = null!;
    public int CategoryId { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public Category Category { get; set; } = null!;
}