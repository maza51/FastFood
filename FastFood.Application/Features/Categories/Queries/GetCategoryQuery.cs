using AutoMapper;
using FastFood.Application.Common;
using FastFood.Application.Interfaces;
using MediatR;

namespace FastFood.Application.Features.Categories.Queries;

public record GetCategoryQuery(PagedParameters PagedParameters) : IRequest<IPagedList<CategoryDto>>;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, IPagedList<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IPagedList<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Categories.GetPagedListAsync(request.PagedParameters);

        return _mapper.Map<IPagedList<CategoryDto>>(categories);
    }
}