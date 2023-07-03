using AutoMapper;
using FastFood.Domain.Entities;

namespace FastFood.Application.Features.Orders;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderDetailsDto>();
        CreateMap<OrderItemCreateDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}