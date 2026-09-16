using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.DTOs.Peliculas;
using Prueba_Talycap_Ecopetrol.Repositories.Context;

namespace Prueba_Talycap_Ecopetrol.Repositories.Interfaces;

/// <summary>
/// Contrato de acceso a datos para la tabla Peliculas.
/// </summary>
public interface IPeliculaRepository
{
    /// <summary>
    /// Obtiene todas las películas registradas.
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

    /// <summary>
    /// Proporciona acceso directo al DbContext para consultas con LINQ.
    /// </summary>
    ClientesDbContext Context { get; }
}
