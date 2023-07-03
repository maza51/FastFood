using AutoMapper;
using FastFood.Application.Common;
using FastFood.Application.Interfaces;
using MediatR;

namespace FastFood.Application.Features.Products.Queries;

public record GetProductNewQuery(PagedParameters PagedParameters) : IRequest<IPagedList<ProductDto>>;

public class GetProductNewQueryHandler : IRequestHandler<GetProductNewQuery, IPagedList<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductNewQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IPagedList<ProductDto>> Handle(GetProductNewQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetNew(request.PagedParameters);

        return _mapper.Map<IPagedList<ProductDto>>(products);
    }
}

