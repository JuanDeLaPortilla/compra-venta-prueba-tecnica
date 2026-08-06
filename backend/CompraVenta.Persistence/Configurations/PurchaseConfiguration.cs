using CompraVenta.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompraVenta.Persistence.Configurations;

public class PurchaseConfiguration : BaseEntityConfiguration<Purchase>
{
    public override void Configure(EntityTypeBuilder<Purchase> builder)
    {
        base.Configure(builder);
    }
}
