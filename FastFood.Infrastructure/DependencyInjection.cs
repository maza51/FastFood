using FastFood.Application.Interfaces;
using FastFood.Infrastructure.EntityFramework;
using FastFood.Infrastructure.Options;
using FastFood.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Sqlite"));
            options.EnableSensitiveDataLogging();
        });
        
        services.AddDbContext<BackupDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("BackupSqlite"));
            options.EnableSensitiveDataLogging();
        });

        services.AddHostedService<RestoreDatabaseService>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IImageService, ImageService>();

        services.Configure<ImageOptions>(
            configuration.GetSection(nameof(ImageOptions)));
        
        return services;
    }
}