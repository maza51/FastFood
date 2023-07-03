using FastFood.Application.Common;
using FastFood.Domain.Entities;

namespace FastFood.Application.Interfaces.Repository;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order?> GetDetailsByIdAsync(int id);
    Task<IPagedList<Order>> GetDetailed(PagedParameters pagedParameters);
    Task<IPagedList<Order>> GetActive();
    Task<IPagedList<Order>> GetActive(PagedParameters pagedParameters);
    Task<IPagedList<Order>> GetActiveDetailed(PagedParameters pagedParameters);
    Task<IList<Order>> GetByDateAsync(DateOnly date);
}