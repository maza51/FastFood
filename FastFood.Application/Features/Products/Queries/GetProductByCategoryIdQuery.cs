using AutoMapper;
using FastFood.Application.Common;
using FastFood.Application.Interfaces;
using MediatR;

namespace FastFood.Application.Features.Products.Queries;

public record GetProductByCategoryIdQuery(int CategoryId, PagedParameters PagedParameters) : IRequest<IPagedList<ProductDto>>;

public class
    GetProductByCategoryIdQueryHandler : IRequestHandler<GetProductByCategoryIdQuery,
        IPagedList<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByCategoryIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IPagedList<ProductDto>> Handle(GetProductByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetByCategory(request.CategoryId, request.PagedParameters);

        return _mapper.Map<IPagedList<ProductDto>>(products);
    }
}