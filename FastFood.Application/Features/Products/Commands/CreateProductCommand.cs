using AutoMapper;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FastFood.Application.Features.Products.Commands;

public class CreateProductCommand : IRequest<ProductDto>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public string ImageBase64 { get; set; } = null!;
    public int CategoryId { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        
        product.ImagePath = await _imageService.SaveAsync(request.ImageBase64);
        
        product.CreatedAt = DateTime.Now;
        
        _unitOfWork.Products.Create(product);
        
        await _unitOfWork.SaveChangeAsync();

        return _mapper.Map<ProductDto>(product);
    }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ImageBase64).NotEmpty();
        RuleFor(x => x.UnitPrice).PrecisionScale(18, 2, false).NotEqual(0);
        RuleFor(x => x.CategoryId).NotEqual(0);
    }
}