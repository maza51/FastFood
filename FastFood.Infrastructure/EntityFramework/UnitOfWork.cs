using FastFood.Application.Interfaces;
using FastFood.Application.Interfaces.Repository;
using FastFood.Infrastructure.EntityFramework.Repositories;

namespace FastFood.Infrastructure.EntityFramework;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private readonly IProductRepository? _productRepository;
    private readonly ICategoryRepository? _categoryRepository;
    private readonly IOrderRepository? _orderRepository;
    private readonly IOrderItemRepository? _orderItemRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _productRepository = null;
        _categoryRepository = null;
        _orderRepository = null;
        _orderItemRepository = null;
    }

    public IProductRepository Products => _productRepository ?? new ProductRepository(_context);
    public ICategoryRepository Categories => _categoryRepository ?? new CategoryRepository(_context);
    public IOrderRepository Orders => _orderRepository ?? new OrderRepository(_context);
    public IOrderItemRepository OrderItems => _orderItemRepository ?? new OrderItemRepository(_context);

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
 
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}