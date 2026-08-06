using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Persistence.Context;

namespace CompraVenta.Commerce.Infrastructure.Repositories;

public class UnitOfWork(CompraVentaDbContext db) : IUnitOfWork
{
    public IProductRepository Products { get; } = new ProductRepository(db);
    public IPurchaseRepository Purchases { get; } = new PurchaseRepository(db);
    
    public void Dispose() => db.Dispose();

    public async Task ReloadAsync(object entity) => await db.Entry(entity).ReloadAsync();
    public async Task AddAsync(object entity) => await db.AddAsync(entity);
    public async Task AddRangeAsync(IEnumerable<object> entities) => await db.AddRangeAsync(entities);

    public async Task SaveChangesInTransactionAsync()
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            await db.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task ExecuteTransactionAsync(Func<Task> action)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            await action();
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> action)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var result = await action();

            await db.SaveChangesAsync();

            await transaction.CommitAsync();

            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}