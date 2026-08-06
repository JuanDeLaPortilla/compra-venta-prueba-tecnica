using CompraVenta.Domain.Constants;
using CompraVenta.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompraVenta.Domain.Entities;

[Table("CompraCab")]
public class Purchase: IIdentifier
{
    [Column("Id_CompraCab")]
    public int Id { get; set; }

    [Required]
    [Column("FecRegistro")]
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
    public virtual ICollection<PurchaseDetail>? PurchaseDetails { get; set; } = [];

    // Methods
    public static Purchase Create(IEnumerable<PurchaseItem> items,
    IReadOnlyDictionary<int, Product> products)
    { 
        var purchase = new Purchase
        {
            RegistrationDate = DateTime.Now,
            PurchaseDetails = []
        };

        var subTotal = 0m;
        var taxAmount = 0m;
        var totalAmount = 0m;

        foreach (var item in items)
        {
            var product = products[item.ProductId];

            product.UpdateCost(item.UnitPrice);

            var detail = PurchaseDetail.Create(
                product,
                item.Quantity,
                item.UnitPrice);

            purchase.PurchaseDetails.Add(detail);

            subTotal += detail.SubTotal;
            taxAmount += detail.TaxAmount;
            totalAmount += detail.TotalAmount;
        }

        purchase.SubTotal = subTotal;
        purchase.TaxAmount = taxAmount;
        purchase.TotalAmount = totalAmount;

        return purchase;
    }
}

public sealed class PurchaseItem
{
    public int ProductId { get; init; }

    public int Quantity { get; init; }

    public decimal UnitPrice { get; init; }
}