using AutoMapper;
using FastFood.Application.Common;
using FastFood.Application.Interfaces;
using MediatR;

namespace FastFood.Application.Features.Products.Queries;

public record GetProductQuery(PagedParameters PagedParameters) : IRequest<IPagedList<ProductDto>>;

public class GetProductQueryHandle : IRequestHandler<GetProductQuery, IPagedList<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IPagedList<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetPagedListAsync(request.PagedParameters);

        return _mapper.Map<IPagedList<ProductDto>>(products);
    }
}