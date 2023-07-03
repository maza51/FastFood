using FastFood.Application.Interfaces.Repository;

namespace FastFood.Application.Interfaces;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    Task<int> SaveChangeAsync();
}