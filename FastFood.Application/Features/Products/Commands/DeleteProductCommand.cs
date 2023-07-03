using FastFood.Application.Exceptions;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using MediatR;

namespace FastFood.Application.Features.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWOrk;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWOrk)
    {
        _unitOfWOrk = unitOfWOrk;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWOrk.Products.GetByIdAsync(request.Id);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product));
        }

        _unitOfWOrk.Products.Delete(product);

        await _unitOfWOrk.SaveChangeAsync();
    }
}