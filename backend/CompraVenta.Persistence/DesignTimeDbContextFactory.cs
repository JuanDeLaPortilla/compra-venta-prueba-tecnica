using CompraVenta.Domain.Constants;
using CompraVenta.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompraVenta.Persistence;

public class DesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<CompraVentaDbContext>
{
    public CompraVentaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CompraVentaDbContext>();

        optionsBuilder.UseSqlServer(PersistenceConstants.CompraVentaConnectionString);

        return new CompraVentaDbContext(optionsBuilder.Options);
    }
}