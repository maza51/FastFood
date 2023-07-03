using FastFood.Domain.Entities.Base;

namespace FastFood.Domain.Entities;

public class Category : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}