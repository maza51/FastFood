using FastFood.Application.Common;

namespace FastFood.Presentation.Models;

public class PagedResponse<T>
{
    public PagedResponse(IPagedList<T> data)
    {
        PageIndex = data.PageIndex;
        PageSize = data.PageSize;
        TotalCount = data.TotalCount;
        Data = data;
    }
    
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public IPagedList<T> Data { get; set; }
}