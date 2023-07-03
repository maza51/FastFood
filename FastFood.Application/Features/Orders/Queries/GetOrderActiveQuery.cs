using AutoMapper;
using FastFood.Application.Common;
using FastFood.Application.Interfaces;
using MediatR;

namespace FastFood.Application.Features.Orders.Queries;

public record GetOrderActiveQuery(PagedParameters PagedParameters) : IRequest<IPagedList<OrderDto>>;

public class GetOrderActiveQueryHandler : IRequestHandler<GetOrderActiveQuery, IPagedList<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderActiveQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IPagedList<OrderDto>> Handle(GetOrderActiveQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetActive(request.PagedParameters);

        return _mapper.Map<IPagedList<OrderDto>>(orders);
    }
}