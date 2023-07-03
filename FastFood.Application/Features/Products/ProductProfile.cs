using AutoMapper;
using FastFood.Application.Features.Products.Commands;
using FastFood.Domain.Entities;

namespace FastFood.Application.Features.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductCommand, Product>();
    }
}