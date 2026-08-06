using System.Linq.Expressions;
using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompraVenta.Commerce.Infrastructure.Repositories;

public class Repository<T>(DbContext context) : IRepository<T> where T : class, IIdentifier
{
    private DbSet<T> DbSet => context.Set<T>();

    public async Task<T?> GetAsync(int id) =>
        await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeProperties = null)
    {
        IQueryable<T> query = DbSet.DefaultIfEmpty()!;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            query = includeProperties
                .Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        
        return await query.ToListAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null)
    {
        IQueryable<T> query = DbSet.DefaultIfEmpty()!;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            query = includeProperties
                .Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        
        return await query.FirstOrDefaultAsync();
    }
}