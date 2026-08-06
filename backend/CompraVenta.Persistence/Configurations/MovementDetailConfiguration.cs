using CompraVenta.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompraVenta.Persistence.Configurations;

public class MovementDetailConfiguration : BaseEntityConfiguration<MovementDetail>
{
    public override void Configure(EntityTypeBuilder<MovementDetail> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Movement)
            .WithMany(x => x.MovementDetails)
            .HasForeignKey(x => x.MovementId);
    }
}
