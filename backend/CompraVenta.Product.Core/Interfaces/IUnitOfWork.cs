namespace CompraVenta.Commerce.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IProductRepository Products { get; }
    public IPurchaseRepository Purchases { get; }
    
    Task ReloadAsync(object entity);
    Task AddAsync(object entity);
    Task AddRangeAsync(IEnumerable<object> entities);

    Task SaveChangesInTransactionAsync();
    Task ExecuteTransactionAsync(Func<Task> action);
    Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> action);
}