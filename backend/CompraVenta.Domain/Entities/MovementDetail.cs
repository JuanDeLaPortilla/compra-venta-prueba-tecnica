using CompraVenta.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompraVenta.Domain.Entities;

[Table("Movimientodet")]
public class MovementDetail : IIdentifier
{
    [Column("Id_MovimientoDet")]
    public int Id { get; set; }

    [Required]
    [Column("Id_movimientocab")]
    public int MovementId { get; set; }

    [Required]
    [Column("Id_Producto")]
    public int ProductId { get; set; }

    [Required]
    [Column("Cantidad")]
    public int Quantity { get; set; }

    // Navigation
    public virtual Movement? Movement { get; set; }
    public virtual Product? Product { get; set; }

    // Methods
    public static MovementDetail Create(int productId, int quantity)
    {
        return new MovementDetail
        {
            ProductId = productId,
            Quantity = quantity
        };
    }
}
