using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Talycap_Ecopetrol.Repositories.Entities;

/// <summary>
/// Entidad que mapea la tabla dbo.Ciudades. Contiene las ciudades
/// consultadas y las coordenadas necesarias para obtener el clima.
/// </summary>
[Table("Ciudades", Schema = "dbo")]
public class Ciudad
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CiudadID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Pais { get; set; } = string.Empty;

    public double? Latitud { get; set; }

    public double? Longitud { get; set; }

    /// <summary>
    /// Registros de clima asociados a la ciudad.
    /// </summary>
    public ICollection<RegistroClima> RegistrosClima { get; set; } = new List<RegistroClima>();
}
