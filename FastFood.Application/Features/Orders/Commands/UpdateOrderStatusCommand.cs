using AutoMapper;
using FastFood.Application.Exceptions;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FluentValidation;
using MediatR;

namespace FastFood.Application.Features.Orders.Commands;

public record UpdateOrderStatusCommand(int Id, OrderStatus Status) : IRequest<OrderDto>;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.Id);

        if (order is null)
        {
            throw new NotFoundException(nameof(Order));
        }

        order.Status = request.Status;

        await _unitOfWork.SaveChangeAsync();

        return _mapper.Map<OrderDto>(order);
    }
}

public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusValidator()
    {
        RuleFor(x => x.Id).Must(x => x != 0);
        RuleFor(x => x.Status).IsInEnum();
    }
}