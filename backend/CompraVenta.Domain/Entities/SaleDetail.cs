using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompraVenta.Domain.Constants;
using CompraVenta.Domain.Entities.Interfaces;

namespace CompraVenta.Domain.Entities;

[Table("VentaDet")]
public class SaleDetail : IIdentifier
{
    [Column("Id_VentaDet")]
    public int Id { get; set; }
    
    [Column("Id_VentaCab")]
    public int SaleId { get; set; }
    
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
    public virtual Product? Product { get; set; }
    public virtual Sale? Sale { get; set; }
    
    // Methods
    public static SaleDetail Create(int productId, int quantity, decimal unitPrice)
    {
        var subTotal = quantity * unitPrice;
        var taxAmount = subTotal * CommerceConstants.TaxRate / 100m;
        var totalAmount = subTotal + taxAmount;

        return new SaleDetail
        {
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            SubTotal = subTotal,
            TaxAmount = taxAmount,
            TotalAmount = totalAmount
        };
    }
}