using CompraVenta.Commerce.Core.Business.Sales.List;
using CompraVenta.Domain.Entities;

namespace CompraVenta.Commerce.Core.Interfaces;

public interface ISaleRepository : IRepository<Sale>
{
    Task<List<SaleResponse>> ListSalesAsync();
}