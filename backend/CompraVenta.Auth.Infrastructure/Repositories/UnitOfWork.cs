using CompraVenta.Auth.Infrastructure.Repositories.UserRepository;
using CompraVenta.Persistence.Context;

namespace CompraVenta.Auth.Infrastructure.Repositories;

public class UnitOfWork(CompraVentaDbContext db) : IUnitOfWork
{
    public IUserRepository Users { get; } = new UserRepository.UserRepository(db);
    
    public void Dispose() => db.Dispose();
    
    public async Task BeginTransactionAsync() => await db.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync() => await db.Database.CommitTransactionAsync();

    public async Task RollbackTransactionAsync() => await db.Database.RollbackTransactionAsync();

    public async Task SaveChangesAsync() => await db.SaveChangesAsync();

    public async Task ReloadAsync(object entity) => await db.Entry(entity).ReloadAsync();

    public async Task AddAsync(object entity) => await db.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<object> entities) => await db.AddRangeAsync(entities);
}