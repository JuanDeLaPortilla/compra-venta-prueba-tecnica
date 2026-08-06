using CompraVenta.Domain.Constants;
using CompraVenta.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompraVenta.Domain.Entities;

[Table("Productos")]
public class Product : IIdentifier
{
    [Column("Id_producto")]
    public int Id { get; set; }

    [Required]
    [Column("Nombre_producto")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("NroLote")]
    public string BatchNumber { get; set; } = string.Empty;

    [Required]
    [Column("Fec_registro")]
    public DateTime RegistrationDate { get; set; }

    [Required]
    [Column("Costo", TypeName = "decimal(12, 2)")]
    public decimal Cost { get; set; }

    [Required]
    [Column("PrecioVenta", TypeName = "decimal(12,2)")]
    public decimal SalePrice { get; set; }

    // Navigation
    public virtual ICollection<PurchaseDetail>? PurchaseDetails { get; set; } = [];
    public virtual ICollection<SaleDetail>? SaleDetails { get; set; } = [];
    public virtual ICollection<MovementDetail>? MovementDetails { get; set; } = [];

    // Methods
    public static Product Create(string name, string batchNumber)
    {
        return new Product
        {
            Name = name,
            BatchNumber = batchNumber,

            RegistrationDate = DateTime.Now,

            Cost = 0,
            SalePrice = 0
        };
    }

    public void Update(string name, string batchNumber)
    {
        Name = name;
        BatchNumber = batchNumber;
    }

    public void UpdateCost(decimal cost)
    {
        Cost = cost;
        SalePrice = Math.Round(cost * CommerceConstants.SaleRate,
            CommerceConstants.MaximumDecimals);
    }
}
