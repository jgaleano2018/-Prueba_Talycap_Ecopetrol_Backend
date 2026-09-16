using Prueba_Talycap_Ecopetrol.DTOs.Clima;
using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.Repositories.Context;

namespace Prueba_Talycap_Ecopetrol.Repositories.Interfaces;

/// <summary>
/// Contrato de acceso a datos para las tablas Ciudades y RegistrosClima.
/// </summary>
public interface IClimaRepository
{
    /// <summary>
    /// Obtiene el clima de todas las ciudades registradas.
    /// </summary>
    Task<IReadOnlyList<ClimaCiudadDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el clima de todas las ciudades registradas de forma paginada.
    /// </summary>
    /// <param name="paginacion">Parámetros de paginación (page y size).</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task<PagedResultDto<ClimaCiudadDto>> ObtenerPaginadoAsync(
        ConsultaPaginadaRequestDto paginacion,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el clima de las ciudades indicadas por nombre.
    /// </summary>
    /// <param name="nombresCiudades">Nombres de las ciudades a consultar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task<IReadOnlyList<ClimaCiudadDto>> ObtenerPorCiudadesAsync(
        IEnumerable<string> nombresCiudades,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Proporciona acceso directo al DbContext para consultas con LINQ.
    /// </summary>
    ClientesDbContext Context { get; }
}
