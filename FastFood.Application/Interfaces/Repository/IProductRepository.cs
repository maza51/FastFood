using FastFood.Application.Common;
using FastFood.Domain.Entities;

namespace FastFood.Application.Interfaces.Repository;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IPagedList<Product>> GetByCategory(int categoryId, PagedParameters pagedParameters);
    Task<IPagedList<Product>> GetNew(PagedParameters pagedParameters);
}