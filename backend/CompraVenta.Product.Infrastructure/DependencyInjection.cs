using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Commerce.Infrastructure.Repositories;
using CompraVenta.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompraVenta.Commerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Persistence
        services.AddPersistence(configuration);
        // End Persistence
        
        // Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // End Repositories
        
        return services;
    }
}