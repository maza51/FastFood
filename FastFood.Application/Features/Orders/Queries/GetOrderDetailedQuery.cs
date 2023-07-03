using AutoMapper;
using FastFood.Application.Common;
using FastFood.Application.Interfaces;
using MediatR;

namespace FastFood.Application.Features.Orders.Queries;

public record GetOrderDetailedQuery(PagedParameters PagedParameters) : IRequest<IPagedList<OrderDetailsDto>>;

public class GetOrderDetailedQueryHandler : IRequestHandler<GetOrderDetailedQuery, IPagedList<OrderDetailsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderDetailedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IPagedList<OrderDetailsDto>> Handle(GetOrderDetailedQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetDetailed(request.PagedParameters);

        return _mapper.Map<IPagedList<OrderDetailsDto>>(orders);
    }
}