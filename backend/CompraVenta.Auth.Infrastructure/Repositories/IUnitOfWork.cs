using CompraVenta.Auth.Infrastructure.Repositories.UserRepository;

namespace CompraVenta.Auth.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository Users { get; }
    
    Task SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task ReloadAsync(object entity);
    Task AddAsync(object entity);
    Task AddRangeAsync(IEnumerable<object> entities);
}