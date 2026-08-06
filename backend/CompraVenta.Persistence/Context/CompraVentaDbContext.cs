using CompraVenta.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompraVenta.Persistence.Context;

public class CompraVentaDbContext : DbContext
{
    public CompraVentaDbContext(DbContextOptions<CompraVentaDbContext> options)
        : base(options)
    {
    }

    // AUTH
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompraVentaDbContext).Assembly);
    }
}