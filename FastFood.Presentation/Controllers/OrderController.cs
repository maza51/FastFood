using FastFood.Application.Common;
using FastFood.Application.Exceptions;
using FastFood.Application.Features.Orders;
using FastFood.Application.Features.Orders.Commands;
using FastFood.Application.Features.Orders.Queries;
using FastFood.Presentation.Models;
using FastFood.Presentation.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Presentation.Controllers;

[Route("/api/orders")]
public class OrderController : Controller
{
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;

    public OrderController(IMediator mediator, INotificationService notificationService)
    {
        _mediator = mediator;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PagedParameters pagedParameters)
    {
        var orders = await _mediator.Send(new GetOrderQuery(pagedParameters));

        return Ok(new PagedResponse<OrderDto>(orders));
    }

    [HttpGet("detailed")]
    public async Task<IActionResult> GetDetailed([FromQuery] PagedParameters pagedParameters)
    {
        var orders = await _mediator.Send(new GetOrderDetailedQuery(pagedParameters));
        
        return Ok(new PagedResponse<OrderDetailsDto>(orders));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive([FromQuery] PagedParameters pagedParameters)
    {
        var orders = await _mediator.Send(new GetOrderActiveQuery(pagedParameters));

        return Ok(new PagedResponse<OrderDto>(orders));
    }

    [HttpGet("active-detailed")]
    public async Task<IActionResult> GetActiveDetailed([FromQuery] PagedParameters pagedParameters)
    {
        var orders = await _mediator.Send(new GetOrderActiveDetailedQuery(pagedParameters));

        return Ok(new PagedResponse<OrderDetailsDto>(orders));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDetails([FromRoute] int id)
    {
        var order = await _mediator.Send(new GetOrderDetailsQuery(id));

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var order = await _mediator.Send(command);
        
        await _notificationService.OrdersChanged();

        return Ok(order);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromBody] UpdateStatusModel? model)
    {
        if (model is null)
        {
            throw new BadRequestException(nameof(UpdateStatusModel));
        }
        
        var order = await _mediator.Send(new UpdateOrderStatusCommand(id, model.Status));
        
        await _notificationService.OrdersChanged();

        return Ok(order);
    }
}