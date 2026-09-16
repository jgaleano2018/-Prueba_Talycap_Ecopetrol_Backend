using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Talycap_Ecopetrol.Repositories.Entities;

/// <summary>
/// Entidad que mapea la tabla dbo.Peliculas de la base de datos DBClientes.
/// </summary>
[Table("Peliculas", Schema = "dbo")]
public class Pelicula
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PeliculaID { get; set; }

    [Required]
    [MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Genero { get; set; }

    public int? Anio { get; set; }

    [MaxLength(150)]
    public string? Director { get; set; }

    /// <summary>
    /// Duración de la película en minutos.
    /// </summary>
    public int? Duracion { get; set; }

    [MaxLength(500)]
    public string? Sinopsis { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }
}
