namespace Prueba_Talycap_Ecopetrol.DTOs.Comunes;

/// <summary>
/// DTO estándar para respuestas de error de la API.
/// </summary>
public class ErrorResponseDto
{
    public int StatusCode { get; set; }

    public string Mensaje { get; set; } = string.Empty;

    public string? Detalle { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;
}
