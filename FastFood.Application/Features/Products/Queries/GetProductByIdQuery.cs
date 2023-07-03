using AutoMapper;
using FastFood.Application.Exceptions;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FastFood.Application.Features.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product));
        }
        
        return _mapper.Map<ProductDto>(product);
    }
}

public sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(0);
    }
}