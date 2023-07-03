using FastFood.Domain.Enums;

namespace FastFood.Application.Features.Orders;

public class OrderDto
{
    public int Id { get; set; }
    public string BuyerCode { get; set; } = null!;
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}