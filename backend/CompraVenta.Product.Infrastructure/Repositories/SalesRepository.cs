using CompraVenta.Commerce.Core.Business.Sales.List;
using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Domain.Entities;
using CompraVenta.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CompraVenta.Commerce.Infrastructure.Repositories;

public class SalesRepository(CompraVentaDbContext db) : Repository<Sale>(db), ISaleRepository
{
    public async Task<List<SaleResponse>> ListSalesAsync()
    {
        return await db.Sales
            .Select(x => new SaleResponse
            {
                Id = x.Id,
                RegistrationDate = x.RegistrationDate,
                SubTotal = x.SubTotal,
                TaxAmount = x.TaxAmount,
                TotalAmount = x.TotalAmount,
            }).ToListAsync();
    }
}