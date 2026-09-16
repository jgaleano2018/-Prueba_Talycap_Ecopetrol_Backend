namespace Prueba_Talycap_Ecopetrol.DTOs.Comunes;

/// <summary>
/// Resultado paginado genérico devuelto por los endpoints de listado.
/// </summary>
/// <typeparam name="T">Tipo de los elementos de la página.</typeparam>
public class PagedResultDto<T>
{
    /// <summary>Número de página actual (base 0).</summary>
    public int Page { get; set; }

    /// <summary>Cantidad de elementos solicitados por página.</summary>
    public int Size { get; set; }

    /// <summary>Cantidad total de elementos disponibles.</summary>
    public int TotalRegistros { get; set; }

    /// <summary>Cantidad total de páginas disponibles.</summary>
    public int TotalPaginas { get; set; }

    /// <summary>Indica si existe una página anterior a la actual.</summary>
    public bool TienePaginaAnterior { get; set; }

    /// <summary>Indica si existe una página siguiente a la actual.</summary>
    public bool TienePaginaSiguiente { get; set; }

    /// <summary>Elementos contenidos en la página actual.</summary>
    public IReadOnlyList<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Construye el resultado paginado a partir de los elementos de la
    /// página, el total de registros y los parámetros de paginación.
    /// </summary>
    public static PagedResultDto<T> Crear(
        IReadOnlyList<T> items,
        int totalRegistros,
        ConsultaPaginadaRequestDto paginacion)
    {
        var totalPaginas = paginacion.Size > 0
            ? (int)Math.Ceiling(totalRegistros / (double)paginacion.Size)
            : 0;

        return new PagedResultDto<T>
        {
            Page = paginacion.Page,
            Size = paginacion.Size,
            TotalRegistros = totalRegistros,
            TotalPaginas = totalPaginas,
            TienePaginaAnterior = paginacion.Page > 0,
            TienePaginaSiguiente = paginacion.Page + 1 < totalPaginas,
            Items = items
        };
    }
}
