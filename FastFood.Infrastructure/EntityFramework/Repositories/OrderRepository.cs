using FastFood.Application.Common;
using FastFood.Application.Interfaces.Repository;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Infrastructure.EntityFramework.Extensions;
using FastFood.Infrastructure.EntityFramework.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.EntityFramework.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Order?> GetDetailsByIdAsync(int id)
    {
        return await Context.Orders.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IPagedList<Order>> GetDetailed(PagedParameters pagedParameters)
    {
        return await Context.Orders.Include(x => x.OrderItems)
            .ToPagedListAsync(pagedParameters.PageIndex, pagedParameters.PageSize);
    }
    
    public async Task<IPagedList<Order>> GetActive()
    {
        var pagedParameters = new PagedParameters();
        return await GetActive(pagedParameters);
    }

    public async Task<IPagedList<Order>> GetActive(PagedParameters pagedParameters)
    {
        return await Context.Orders
            .Where(x => x.Status == OrderStatus.Processing || x.Status == OrderStatus.Ready)
            .ToPagedListAsync(pagedParameters.PageIndex, pagedParameters.PageSize);
    }

    public async Task<IPagedList<Order>> GetActiveDetailed(PagedParameters pagedParameters)
    {
        return await Context.Orders.Include(x => x.OrderItems)
            .Where(x => x.Status == OrderStatus.Processing || x.Status == OrderStatus.Ready)
            .ToPagedListAsync(pagedParameters.PageIndex, pagedParameters.PageSize);
    }

    public async Task<IList<Order>> GetByDateAsync(DateOnly date)
    {
        return await Context.Orders.Where(x =>
            x.CreatedAt.Date.Year == date.Year &&
            x.CreatedAt.Date.Month == date.Month &&
            x.CreatedAt.Date.Day == date.Day).ToListAsync();
    }
}