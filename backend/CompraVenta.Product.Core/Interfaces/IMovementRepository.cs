using CompraVenta.Commerce.Core.Business.Kardex.List;
using CompraVenta.Commerce.Core.Business.Kardex.ListProductMovements;
using CompraVenta.Domain.Entities;

namespace CompraVenta.Commerce.Core.Interfaces;

public interface IMovementRepository : IRepository<Movement>
{
    Task<List<KardexResponse>> ListKardexAsync();
    Task<List<ProductMovementResponse>> ListProductMovementsAsync(int productId);
}