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

    // PRODUCTS
    public DbSet<Product> Products => Set<Product>();

    // MOVEMENTS
    public DbSet<Movement> Movements => Set<Movement>();
    public DbSet<MovementDetail> MovementDetails => Set<MovementDetail>();

    // PURCHASES
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseDetail> PurchaseDetails => Set<PurchaseDetail>();
    
    // SALES
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleDetail> SaleDetails => Set<SaleDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompraVentaDbContext).Assembly);
    }
}