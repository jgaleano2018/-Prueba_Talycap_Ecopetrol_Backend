namespace Prueba_Talycap_Ecopetrol.DTOs.Peliculas;

/// <summary>
/// DTO de salida con los datos de una película.
/// </summary>
public class PeliculaDto
{
    public int PeliculaID { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string? Genero { get; set; }

    public int? Anio { get; set; }

    public string? Director { get; set; }

    /// <summary>
    /// Duración de la película en minutos.
    /// </summary>
    public int? Duracion { get; set; }

    public string? Sinopsis { get; set; }

    public DateTime FechaRegistro { get; set; }
}
