using CompraVenta.Domain.Entities.Interfaces;
using System.Linq.Expressions;

namespace CompraVenta.Commerce.Core.Interfaces
{
    public interface IRepository<T> where T : IIdentifier
    {
        Task<T?> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null);

        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null);
    }
}