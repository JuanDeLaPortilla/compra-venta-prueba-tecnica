using CompraVenta.Commerce.Core.Business.Purchases.List;
using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Domain.Entities;
using CompraVenta.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CompraVenta.Commerce.Infrastructure.Repositories;

public class PurchaseRepository(CompraVentaDbContext db) : Repository<Purchase>(db), IPurchaseRepository
{
    public async Task<List<PurchaseResponse>> ListPurchasesAsync()
    {
        return await db.Purchases
            .Select(x => new PurchaseResponse
            {
                Id = x.Id,
                RegistrationDate = x.RegistrationDate,
                SubTotal = x.SubTotal,
                TaxAmount = x.TaxAmount,
                TotalAmount = x.TotalAmount,
            }).ToListAsync();
    }
}