using FastFood.Application.Interfaces.Repository;
using FastFood.Domain.Entities;
using FastFood.Infrastructure.EntityFramework.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.EntityFramework.Repositories;

public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
    {
        return await Context.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();
    }
}