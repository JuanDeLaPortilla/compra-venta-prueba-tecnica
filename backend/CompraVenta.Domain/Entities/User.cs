using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompraVenta.Domain.Entities.Interfaces;

namespace CompraVenta.Domain.Entities;

[Table("Usuarios")]
public class User : IIdentifier
{
    [Column("Id_usuario")]
    public int Id { get; set; }
    
    [Required]
    [Column("Nombre_usuario")]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [Column("Contrasena")]
    public string PasswordHash { get; set; } = string.Empty;
}