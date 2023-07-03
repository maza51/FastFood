using FastFood.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FastFood.Infrastructure.Services;

public class RestoreDatabaseService : BackgroundService
{
    private readonly BackupDbContext _backupDbContext;
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<RestoreDatabaseService> _logger;

    public RestoreDatabaseService(IConfiguration configuration, ILogger<RestoreDatabaseService> logger)
    {
        var backupDbContextOptions = new DbContextOptionsBuilder<BackupDbContext>()
            .UseSqlite(configuration.GetConnectionString("BackupSqlite"))
            .Options;
        
        var appDbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(configuration.GetConnectionString("Sqlite"))
            .Options;
        
        _backupDbContext = new BackupDbContext(backupDbContextOptions);
        _appDbContext = new AppDbContext(appDbContextOptions);
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTimeOffset.Now;
            var nextHour = now.AddHours(1)
                .AddMinutes(-now.Minute)
                .AddSeconds(-now.Second)
                .AddMilliseconds(-now.Millisecond);

            var timeRemaining = nextHour - now;
            
            await Task.Delay(timeRemaining, stoppingToken);

            await RestoreAsync();
        }
    }

    private async Task RestoreAsync()
    {
        _logger.LogInformation("Restore Database....");
        
        await _appDbContext.Database.ExecuteSqlRawAsync("DELETE FROM Products; DELETE FROM Orders");
        
        var productsTask = _backupDbContext.Products.ToListAsync();
        var ordersTask = _backupDbContext.Orders.ToListAsync();
        var orderItemsTask = _backupDbContext.OrderItems.ToListAsync();

        await Task.WhenAll(productsTask, ordersTask, orderItemsTask);

        _appDbContext.Products.AddRange(productsTask.Result);
        _appDbContext.Orders.AddRange(ordersTask.Result);
        _appDbContext.OrderItems.AddRange(orderItemsTask.Result);

        await _appDbContext.SaveChangesAsync();
        
        _logger.LogInformation("Database Restored");
    }
}