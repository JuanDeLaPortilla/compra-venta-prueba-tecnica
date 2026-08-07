using CompraVenta.Domain.Constants;
using CompraVenta.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompraVenta.Domain.Entities;

[Table("CompraDet")]
public class PurchaseDetail : IIdentifier
{
    [Column("Id_CompraDet")]
    public int Id { get; set; }

    [Required]
    [Column("Id_CompraCab")]
    public int PurchaseId { get; set; }

    [Required]
    [Column("Id_producto")]
    public int ProductId { get; set; }

    [Required]
    [Column("Cantidad")]
    public int Quantity { get; set; }

    [Required]
    [Column("Precio", TypeName = "decimal(12, 2)")]
    public decimal UnitPrice { get; set; }

    [Required]
    [Column("Sub_Total", TypeName = "decimal(12, 2)")]
    public decimal SubTotal { get; set; }

    [Required]
    [Column("Igv", TypeName = "decimal(12, 2)")]
    public decimal TaxAmount { get; set; }

    [Required]
    [Column("Total", TypeName = "decimal(12, 2)")]
    public decimal TotalAmount { get; set; }

    // Navigation
    public virtual Purchase? Purchase { get; set; }
    public virtual Product? Product { get; set; }

    // Methods
    public static PurchaseDetail Create(Product product, int quantity, decimal unitPrice)
    {
        var subTotal = quantity * unitPrice;
        var taxAmount = subTotal * CommerceConstants.TaxRate;
        var totalAmount = subTotal + taxAmount;

        return new PurchaseDetail
        {
            Product = product,
            Quantity = quantity,
            UnitPrice = unitPrice,
            SubTotal = subTotal,
            TaxAmount = taxAmount,
            TotalAmount = totalAmount
        };
    }
}
