using CompraVenta.Commerce.Core.Business.Products.List;
using CompraVenta.Domain.Entities;

namespace CompraVenta.Commerce.Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<ProductResponse>> ListProductsAsync();
}
