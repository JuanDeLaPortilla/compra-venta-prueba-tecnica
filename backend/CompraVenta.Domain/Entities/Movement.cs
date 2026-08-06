using CompraVenta.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompraVenta.Domain.Entities;

[Table("MovimientoCab")]
public class Movement : IIdentifier
{
    public enum MovementType
    {
        InBound = 1,
        OutBound = 2,
    }  
    
    [Column("Id_MovimientoCab")]
    public int Id { get; set; }

    [Required]
    [Column("FecRegistro")]
    public DateTime RegistrationDate { get; set; }

    [Required]
    [Column("Id_TipoMovimiento")]
    public MovementType MovementTypeEnum { get; set; }

    [Required]
    [Column("Id_DocumentoOrigen")]
    public int OriginDocumentId { get; set; }

    // Navigation
    public virtual ICollection<MovementDetail> MovementDetails { get; set; } = [];

    // Methods
    public static Movement Create(MovementType movementType, IEnumerable<MovementItem> items)
    {
        return new Movement
        {
            MovementTypeEnum = movementType,
            RegistrationDate = DateTime.Now,

            MovementDetails = items
                .Select(x => MovementDetail.Create(x.ProductId, x.Quantity))
                .ToList()
        };
    }
}

public sealed class MovementItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
