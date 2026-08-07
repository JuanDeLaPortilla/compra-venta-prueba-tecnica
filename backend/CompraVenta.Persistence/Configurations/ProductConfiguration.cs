using CompraVenta.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CompraVenta.Persistence.Configurations;

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        
        // DATA SEEDING
        
        builder.HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop Lenovo IdeaPad 3",
                BatchNumber = "LOT-LEN-001",
                RegistrationDate = new DateTime(2026, 8, 1),
                Cost = 1800.00m,
                SalePrice = 2430.00m
            },
            new Product
            {
                Id = 2,
                Name = "Mouse Logitech M185",
                BatchNumber = "LOT-LOG-001",
                RegistrationDate = new DateTime(2026, 8, 1),
                Cost = 35.00m,
                SalePrice = 47.25m
            },
            new Product
            {
                Id = 3,
                Name = "Teclado Logitech K120",
                BatchNumber = "LOT-LOG-002",
                RegistrationDate = new DateTime(2026, 8, 2),
                Cost = 45.00m,
                SalePrice = 60.75m
            },
            new Product
            {
                Id = 4,
                Name = "Monitor LG 24 pulgadas",
                BatchNumber = "LOT-LG-001",
                RegistrationDate = new DateTime(2026, 8, 2),
                Cost = 550.00m,
                SalePrice = 742.50m
            },
            new Product
            {
                Id = 5,
                Name = "Audífonos Sony WH-CH520",
                BatchNumber = "LOT-SONY-001",
                RegistrationDate = new DateTime(2026, 8, 3),
                Cost = 180.00m,
                SalePrice = 243.00m
            },
            new Product
            {
                Id = 6,
                Name = "Webcam Logitech C920",
                BatchNumber = "LOT-LOG-003",
                RegistrationDate = new DateTime(2026, 8, 3),
                Cost = 280.00m,
                SalePrice = 378.00m
            },
            new Product
            {
                Id = 7,
                Name = "Disco SSD Kingston 1TB",
                BatchNumber = "LOT-KNG-001",
                RegistrationDate = new DateTime(2026, 8, 4),
                Cost = 250.00m,
                SalePrice = 337.50m
            },
            new Product
            {
                Id = 8,
                Name = "Memoria RAM Kingston 16GB",
                BatchNumber = "LOT-KNG-002",
                RegistrationDate = new DateTime(2026, 8, 4),
                Cost = 160.00m,
                SalePrice = 216.00m
            },
            new Product
            {
                Id = 9,
                Name = "Impresora Epson EcoTank L3250",
                BatchNumber = "LOT-EPS-001",
                RegistrationDate = new DateTime(2026, 8, 5),
                Cost = 650.00m,
                SalePrice = 877.50m
            },
            new Product
            {
                Id = 10,
                Name = "Router TP-Link Archer C6",
                BatchNumber = "LOT-TPL-001",
                RegistrationDate = new DateTime(2026, 8, 5),
                Cost = 120.00m,
                SalePrice = 162.00m
            }
        );
    }
}
