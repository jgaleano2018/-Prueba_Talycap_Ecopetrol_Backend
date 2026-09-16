using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Talycap_Ecopetrol.Repositories.Entities;

/// <summary>
/// Entidad que mapea la tabla dbo.RegistrosClima con los datos
/// meteorológicos almacenados por ciudad.
/// </summary>
[Table("RegistrosClima", Schema = "dbo")]
public class RegistroClima
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RegistroClimaID { get; set; }

    public int CiudadID { get; set; }

    public double Temperatura { get; set; }

    public double SensacionTermica { get; set; }

    public int Humedad { get; set; }

    public double VientoVelocidad { get; set; }

    [MaxLength(20)]
    public string? Condicion { get; set; }

    public DateTime FechaConsulta { get; set; }

    [ForeignKey(nameof(CiudadID))]
    public Ciudad? Ciudad { get; set; }
}
