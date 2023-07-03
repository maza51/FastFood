using FastFood.Application.Exceptions;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using MediatR;

namespace FastFood.Application.Features.Categories.Commands;

public record DeleteCategoryCommand(int Id) : IRequest;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);

        if (category is null)
        {
            throw new NotFoundException(nameof(Category));
        }
        
        _unitOfWork.Categories.Delete(category);

        await _unitOfWork.SaveChangeAsync();
    }
}