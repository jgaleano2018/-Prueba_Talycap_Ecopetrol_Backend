namespace Prueba_Talycap_Ecopetrol.DTOs.Comunes;

/// <summary>
/// Parámetros de paginación recibidos por query string (page y size).
/// </summary>
public class ConsultaPaginadaRequestDto
{
    /// <summary>Tamaño de página por defecto cuando no se envía el parámetro size.</summary>
    public const int SizePorDefecto = 10;

    /// <summary>Tamaño de página máximo permitido.</summary>
    public const int SizeMaximo = 100;

    private int _page;
    private int _size = SizePorDefecto;

    /// <summary>
    /// Número de página (base 0). Los valores negativos se normalizan a 0.
    /// </summary>
    public int Page
    {
        get => _page;
        set => _page = value < 0 ? 0 : value;
    }

    /// <summary>
    /// Cantidad de elementos por página. Los valores menores a 1 se
    /// reemplazan por el tamaño por defecto y los mayores al máximo se limitan.
    /// </summary>
    public int Size
    {
        get => _size;
        set => _size = value <= 0
            ? SizePorDefecto
            : Math.Min(value, SizeMaximo);
    }

    /// <summary>Cantidad de elementos a omitir para llegar a la página solicitada.</summary>
    public int Skip => Page * Size;
}
