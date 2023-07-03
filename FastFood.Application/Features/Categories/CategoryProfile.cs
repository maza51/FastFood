using AutoMapper;
using FastFood.Domain.Entities;

namespace FastFood.Application.Features.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}