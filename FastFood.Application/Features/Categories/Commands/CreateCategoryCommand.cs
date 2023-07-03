using AutoMapper;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FastFood.Application.Features.Categories.Commands;

public record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name
        };
        
        _unitOfWork.Categories.Create(category);

        await _unitOfWork.SaveChangeAsync();

        return _mapper.Map<CategoryDto>(category);
    }
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
    }
}