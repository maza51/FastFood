using FastFood.Application.Common;
using FastFood.Application.Interfaces.Repository;
using FastFood.Domain.Entities;
using FastFood.Infrastructure.EntityFramework.Extensions;
using FastFood.Infrastructure.EntityFramework.Repositories.Base;

namespace FastFood.Infrastructure.EntityFramework.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IPagedList<Product>> GetByCategory(int categoryId, PagedParameters pagedParameters)
    {
        return await Context.Products.Where(x => 
            x.CategoryId == categoryId)
            .ToPagedListAsync(pagedParameters.PageIndex, pagedParameters.PageSize);
    }

    public async Task<IPagedList<Product>> GetNew(PagedParameters pagedParameters)
    {
        return await Context.Products.OrderByDescending(x => x.CreatedAt)
            .ToPagedListAsync(pagedParameters.PageIndex, pagedParameters.PageSize);
    }
}