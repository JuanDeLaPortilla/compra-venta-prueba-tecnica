using CompraVenta.Domain.Entities;
using CompraVenta.Util.PasswordManager;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompraVenta.Persistence.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder.HasIndex(x => x.Username).IsUnique();
        
        // DATA SEEDING
        builder.HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = PasswordManager.EncodePassword("4dm1n!")
            }
        );
    }
}