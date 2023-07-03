using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.EntityFramework;

public class BackupDbContext : DbContext
{
    public BackupDbContext(DbContextOptions<BackupDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
}