using AutoMapper;
using FastFood.Application.Common;
using FastFood.Application.Interfaces;
using MediatR;

namespace FastFood.Application.Features.Orders.Queries;

public record GetOrderQuery(PagedParameters PagedParameters) : IRequest<IPagedList<OrderDto>>;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, IPagedList<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IPagedList<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetPagedListAsync(request.PagedParameters);

        return _mapper.Map<IPagedList<OrderDto>>(orders);
    }
}