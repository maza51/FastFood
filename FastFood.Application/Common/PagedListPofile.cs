using AutoMapper;

namespace FastFood.Application.Common;

public class PagedListProfile : Profile
{
    public PagedListProfile()
    {
        CreateMap(typeof(IPagedList<>), typeof(IPagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
    }
}

public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
{
    public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
    {
        var mappedList = context.Mapper.Map<List<TDestination>>(source.ToList());
        
        return new PagedList<TDestination>(mappedList, source.PageIndex, source.PageSize, source.TotalCount);
    }
}