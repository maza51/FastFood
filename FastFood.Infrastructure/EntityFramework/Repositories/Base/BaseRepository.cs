using FastFood.Application.Common;
using FastFood.Application.Interfaces.Repository;
using FastFood.Domain.Entities.Base;
using FastFood.Infrastructure.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.EntityFramework.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    private protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<IPagedList<TEntity>> GetPagedListAsync(PagedParameters pagedParameters)
    {
        return await Context.Set<TEntity>()
            .ToPagedListAsync(pagedParameters.PageIndex, pagedParameters.PageSize);
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }
}