using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompraVenta.Domain.Entities.Interfaces;

namespace CompraVenta.Domain.Entities;

[Table("Ventacab")]
public class Sale : IIdentifier
{
    [Column("Id_VentaCab")]
    public int Id { get; set; }
    
    [Required]
    [Column("fecRegistro")]
    public DateTime RegistrationDate { get; set; }
    
    [Required]
    [Column("SubTotal", TypeName = "decimal(12, 2)")]
    public decimal SubTotal { get; set; }

    [Required]
    [Column("Igv", TypeName = "decimal(12, 2)")]
    public decimal TaxAmount { get; set; }

    [Required]
    [Column("Total", TypeName = "decimal(12, 2)")]
    public decimal TotalAmount { get; set; }
    
    // Navigation
    public virtual ICollection<SaleDetail>? SaleDetails { get; set; } = [];
    
    // Methods
    public static Sale Create(IEnumerable<SaleItem> items)
    {
        var sale = new Sale
        {
            RegistrationDate = DateTime.Now,
            SaleDetails = []
        };
        
        var subTotal = 0m;
        var taxAmount = 0m;
        var totalAmount = 0m;

        foreach (var item in items)
        {
            var detail = SaleDetail.Create(
                item.ProductId,
                item.Quantity,
                item.UnitPrice);

            sale.SaleDetails.Add(detail);

            subTotal += detail.SubTotal;
            taxAmount += detail.TaxAmount;
            totalAmount += detail.TotalAmount;
        }

        sale.SubTotal = subTotal;
        sale.TaxAmount = taxAmount;
        sale.TotalAmount = totalAmount;

        return sale;
    }
}

public sealed class SaleItem
{
    public int ProductId { get; init; }

    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}