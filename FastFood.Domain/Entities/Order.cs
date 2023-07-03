using FastFood.Domain.Entities.Base;
using FastFood.Domain.Enums;

namespace FastFood.Domain.Entities;

public class Order : IEntity
{
    public int Id { get; set; }
    public string BuyerCode { get; set; } = null!;
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}