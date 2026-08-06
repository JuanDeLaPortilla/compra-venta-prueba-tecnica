using CompraVenta.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompraVenta.Persistence.Configurations;

public class PurchaseDetailConfiguration : BaseEntityConfiguration<PurchaseDetail>
{
    public override void Configure(EntityTypeBuilder<PurchaseDetail> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Purchase)
            .WithMany(x => x.PurchaseDetails)
            .HasForeignKey(x => x.PurchaseId);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.PurchaseDetails)
            .HasForeignKey(x => x.ProductId);
    }
}
