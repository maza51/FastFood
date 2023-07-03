using AutoMapper;
using FastFood.Application.Common;
using FastFood.Application.Interfaces;
using MediatR;

namespace FastFood.Application.Features.Orders.Queries;

public record GetOrderActiveDetailedQuery(PagedParameters PagedParameters) : IRequest<IPagedList<OrderDetailsDto>>;

public class GetOrderActiveDetailedQueryHandler : IRequestHandler<GetOrderActiveDetailedQuery, IPagedList<OrderDetailsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderActiveDetailedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IPagedList<OrderDetailsDto>> Handle(GetOrderActiveDetailedQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetActiveDetailed(request.PagedParameters);

        return _mapper.Map<IPagedList<OrderDetailsDto>>(orders);
    }
}