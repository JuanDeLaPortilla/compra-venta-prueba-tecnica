using CompraVenta.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompraVenta.Persistence.Configurations;

public abstract class BaseEntityConfiguration<T> 
    : IEntityTypeConfiguration<T>
    where T : class, IIdentifier
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();
    }
}