using AutoMapper;
using FastFood.Application.Exceptions;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using MediatR;

namespace FastFood.Application.Features.Orders.Queries;

public record GetOrderDetailsQuery(int Id) : IRequest<OrderDetailsDto>;

public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDetailsDto> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetDetailsByIdAsync(request.Id);

        if (order is null)
        {
            throw new NotFoundException(nameof(Order));
        }

        return _mapper.Map<OrderDetailsDto>(order);
    }
}