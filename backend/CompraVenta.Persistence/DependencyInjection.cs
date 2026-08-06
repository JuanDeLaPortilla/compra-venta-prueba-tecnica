using CompraVenta.Domain.Constants;
using CompraVenta.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompraVenta.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CompraVentaDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString(PersistenceConstants.DefaultConnectionKey));
        });

        return services;
    }
}