namespace Prueba_Talycap_Ecopetrol.DTOs.Clima;

/// <summary>
/// DTO de consulta con las ciudades (por nombre) para las que
/// se desea obtener el clima.
/// </summary>
public class ConsultaClimaRequestDto
{
    /// <summary>
    /// Nombres de las ciudades a consultar. Si viene vacío se
    /// devuelven todas las ciudades registradas.
    /// </summary>
    public IReadOnlyList<string> Ciudades { get; set; } = new List<string>();
}
