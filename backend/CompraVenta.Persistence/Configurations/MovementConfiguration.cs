using CompraVenta.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompraVenta.Persistence.Configurations;

public class MovementConfiguration : BaseEntityConfiguration<Movement>
{
    public override void Configure(EntityTypeBuilder<Movement> builder)
    {
        base.Configure(builder);
    }
}
