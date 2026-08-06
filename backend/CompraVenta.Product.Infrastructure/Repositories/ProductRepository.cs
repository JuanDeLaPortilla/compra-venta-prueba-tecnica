using CompraVenta.Commerce.Core.Business.Products.List;
using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Domain.Entities;
using CompraVenta.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using static CompraVenta.Domain.Entities.Movement;

namespace CompraVenta.Commerce.Infrastructure.Repositories;

public class ProductRepository(CompraVentaDbContext db) : Repository<Product>(db), IProductRepository
{
    public async Task<List<ProductResponse>> ListProductsAsync()
    {
        return await GetProductsQuery()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    private IQueryable<ProductResponse> GetProductsQuery()
    {
        return db.Products
            .AsNoTracking()
            .Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                BatchNumber = product.BatchNumber,
                Cost = product.Cost,
                SalePrice = product.SalePrice,

                Stock = db.MovementDetails
                    .Where(x => x.ProductId == product.Id)
                    .Sum(x => x.Movement!.MovementTypeEnum == MovementType.InBound
                        ? x.Quantity
                        : -x.Quantity)
            });
    }
}
