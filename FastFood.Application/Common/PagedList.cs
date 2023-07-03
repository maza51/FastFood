namespace FastFood.Application.Common;

public class PagedList<T> : List<T>, IPagedList<T>
{
    public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        
        AddRange(items);
    }

    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
}