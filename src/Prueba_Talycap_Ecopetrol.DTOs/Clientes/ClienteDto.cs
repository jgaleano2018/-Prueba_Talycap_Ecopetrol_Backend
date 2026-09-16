namespace Prueba_Talycap_Ecopetrol.DTOs.Clientes;

/// <summary>
/// DTO de salida con los datos de un cliente.
/// </summary>
public class ClienteDto
{
    public int ClienteID { get; set; }

    public string Identificacion { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string Apellido { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    public DateTime FechaRegistro { get; set; }
}
