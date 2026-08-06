using CompraVenta.Auth.Infrastructure.Repositories;
using CompraVenta.Auth.Infrastructure.Security;
using CompraVenta.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompraVenta.Auth.Infrastructure;

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
        
        // Security
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        // End Security
        
        return services;
    }
}