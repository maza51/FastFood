namespace FastFood.Application.Features.Orders;

public class OrderDetailsDto : OrderDto
{
    public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}