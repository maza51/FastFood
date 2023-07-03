namespace FastFood.Application.Common;

public class PagedParameters
{
    private const int MaxPageSize = 50;

    private int _pageSize = MaxPageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    //public int PageIndex { get; set; } = 1;
    
    private int _pageIndex = 1;

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value;
    }
    
    public PagedParameters()
    {
        PageSize = MaxPageSize;
        PageIndex = 1;
    }
}