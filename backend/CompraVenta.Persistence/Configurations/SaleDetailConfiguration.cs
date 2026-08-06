using CompraVenta.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompraVenta.Persistence.Configurations;

public class SaleDetailConfiguration : BaseEntityConfiguration<SaleDetail>
{
    public override void Configure(EntityTypeBuilder<SaleDetail> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Sale)
            .WithMany(x => x.SaleDetails)
            .HasForeignKey(x => x.SaleId);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.SaleDetails)
            .HasForeignKey(x => x.ProductId);
    }
}
