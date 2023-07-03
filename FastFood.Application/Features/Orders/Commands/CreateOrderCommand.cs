using AutoMapper;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FluentValidation;
using MediatR;

namespace FastFood.Application.Features.Orders.Commands;

public class CreateOrderCommand : IRequest<OrderDto>
{
    public OrderStatus Status { get; set; }
    public ICollection<OrderItemCreateDto> OrderItems { get; set; } = null!;
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBuyerCodeService _buyerCodeService;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBuyerCodeService buyerCodeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _buyerCodeService = buyerCodeService;
    }
    
    private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1);

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CreatedAt = DateTime.Now,
            Status = request.Status,
            OrderItems = _mapper.Map<List<OrderItem>>(request.OrderItems)
        };

        await SemaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            order.BuyerCode = await _buyerCodeService.GenerateCodeAsync(); // TODO

            _unitOfWork.Orders.Create(order);

            await _unitOfWork.SaveChangeAsync();
        }
        finally
        {
            SemaphoreSlim.Release();
        }

        return _mapper.Map<OrderDto>(order);
    }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.OrderItems).NotNull();
    }
}