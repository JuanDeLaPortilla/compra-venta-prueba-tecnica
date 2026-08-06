using CompraVenta.Commerce.Core.Business.Purchases.List;
using CompraVenta.Domain.Entities;

namespace CompraVenta.Commerce.Core.Interfaces;

public interface IPurchaseRepository : IRepository<Purchase>
{
    Task<List<PurchaseResponse>> ListPurchasesAsync();
}