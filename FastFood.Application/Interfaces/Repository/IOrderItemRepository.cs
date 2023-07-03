using FastFood.Domain.Entities;

namespace FastFood.Application.Interfaces.Repository;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
}