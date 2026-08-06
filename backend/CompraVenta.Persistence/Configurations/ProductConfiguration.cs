using CompraVenta.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CompraVenta.Persistence.Configurations;

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
    }
}
