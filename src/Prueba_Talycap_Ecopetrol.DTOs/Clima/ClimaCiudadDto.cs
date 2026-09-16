namespace Prueba_Talycap_Ecopetrol.DTOs.Clima;

/// <summary>
/// DTO de salida con los datos de clima de una ciudad.
/// </summary>
public class ClimaCiudadDto
{
    public int CiudadID { get; set; }

    public string Ciudad { get; set; } = string.Empty;

    public string Pais { get; set; } = string.Empty;

    public double? Latitud { get; set; }

    public double? Longitud { get; set; }

    public double Temperatura { get; set; }

    public double SensacionTermica { get; set; }

    public int Humedad { get; set; }

    public double VientoVelocidad { get; set; }

    public string? Condicion { get; set; }

    public DateTime FechaConsulta { get; set; }
}
