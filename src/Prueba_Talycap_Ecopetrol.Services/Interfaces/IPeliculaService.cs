using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.DTOs.Peliculas;

namespace Prueba_Talycap_Ecopetrol.Services.Interfaces;

/// <summary>
/// Contrato de lógica de negocio para la consulta de películas.
/// </summary>
public interface IPeliculaService
{
    /// <summary>
    /// Obtiene el listado completo de películas registradas.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task<IReadOnlyList<PeliculaDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el listado de películas paginado según page y size.
    /// </summary>
    /// <param name="paginacion">Parámetros de paginación (page y size).</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task<PagedResultDto<PeliculaDto>> ObtenerPaginadoAsync(
        ConsultaPaginadaRequestDto paginacion,
        CancellationToken cancellationToken = default);
}
