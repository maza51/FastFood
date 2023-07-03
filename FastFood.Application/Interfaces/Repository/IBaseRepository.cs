using FastFood.Application.Common;

namespace FastFood.Application.Interfaces.Repository;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetListAsync();
    Task<IPagedList<TEntity>> GetPagedListAsync(PagedParameters pagedParameters);
    Task<TEntity?> GetByIdAsync(int id);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}