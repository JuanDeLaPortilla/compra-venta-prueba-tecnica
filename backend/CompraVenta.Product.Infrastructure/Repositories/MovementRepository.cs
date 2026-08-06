using CompraVenta.Commerce.Core.Business.Kardex.List;
using CompraVenta.Commerce.Core.Business.Kardex.ListProductMovements;
using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Domain.Entities;
using CompraVenta.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using static CompraVenta.Domain.Entities.Movement;

namespace CompraVenta.Commerce.Infrastructure.Repositories;

public class MovementRepository(CompraVentaDbContext db) : Repository<Movement>(db), IMovementRepository
{
    public async Task<List<KardexResponse>> ListKardexAsync()
    {
        return await db.Products
            .Select(p => new KardexResponse
            {
                ProductId = p.Id,
                ProductName = p.Name,
                Cost = p.Cost,
                SalePrice = p.SalePrice,

                Stock = p.MovementDetails != null && p.MovementDetails.Count > 0
                    ? p.MovementDetails!.Sum(m =>
                        m.Movement!.MovementTypeEnum == MovementType.InBound
                            ? m.Quantity
                            : -m.Quantity)
                    : 0
            })
            .OrderBy(x => x.ProductName)
            .ToListAsync();
    }

    public async Task<List<ProductMovementResponse>> ListProductMovementsAsync(int productId)
    {
        return await db.MovementDetails
            .Where(x => x.ProductId == productId)
            .OrderByDescending(x => x.Movement!.RegistrationDate)
            .Select(x => new ProductMovementResponse
            {
                RegistrationDate = x.Movement!.RegistrationDate,
                MovementType = x.Movement.MovementTypeName,
                Quantity = x.Quantity
            })
            .ToListAsync();
    }
}