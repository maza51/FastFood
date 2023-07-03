using FastFood.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.EntityFramework.Extensions;

public static class QueryableExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageIndex, int pageSize)
    {
        var totalCount = await queryable.CountAsync();
        var takenList = await queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(takenList, pageIndex, pageSize, totalCount);
    }
}