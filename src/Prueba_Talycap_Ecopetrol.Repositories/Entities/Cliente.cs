using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Talycap_Ecopetrol.Repositories.Entities;

/// <summary>
/// Entidad que mapea la tabla dbo.Clientes de la base de datos DBClientes.
/// </summary>
[Table("Clientes", Schema = "dbo")]
public class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClienteID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }

    [Required]
    [MaxLength(50)]
    public string Identificacion { get; set; } = string.Empty;
}
