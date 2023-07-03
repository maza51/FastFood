namespace FastFood.Application.Common;

public interface IPagedList<T> : IList<T>
{
    int PageIndex { get; }
    int PageSize { get; }
    int TotalCount { get; }
}