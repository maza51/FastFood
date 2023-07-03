using AutoMapper;
using FastFood.Application.Exceptions;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FastFood.Application.Features.Products.Commands;

public class UpdateProductCommand : IRequest<ProductDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public string ImageBase64 { get; set; } = null!;
    public int CategoryId { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    
    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imageService = imageService;
    }
    
    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productInDb = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (productInDb is null)
        {
            throw new NotFoundException(nameof(Product));
        }

        if (!await _imageService.IsSameImageAsync(productInDb.ImagePath, request.ImageBase64))
        {
            productInDb.ImagePath = await _imageService.SaveAsync(request.ImageBase64);
        }

        productInDb.Name = request.Name;
        productInDb.Description = request.Description;
        productInDb.UnitPrice = request.UnitPrice;
        productInDb.CategoryId = request.CategoryId;

        _unitOfWork.Products.Update(productInDb);

        await _unitOfWork.SaveChangeAsync();

        return _mapper.Map<ProductDto>(productInDb);
    }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ImageBase64).NotEmpty();
        RuleFor(x => x.UnitPrice).PrecisionScale(18, 2, false).NotEqual(0);
        RuleFor(x => x.CategoryId).NotEqual(0);
    }
}