using Prueba_Talycap_Ecopetrol.DTOs.Clima;
using Prueba_Talycap_Ecopetrol.DTOs.Comunes;

namespace Prueba_Talycap_Ecopetrol.Services.Interfaces;

/// <summary>
/// Contrato de lógica de negocio para la consulta de clima.
/// </summary>
public interface IClimaService
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
    /// Obtiene el clima de varias ciudades indicadas por nombre.
    /// </summary>
    /// <param name="nombresCiudades">Nombres de las ciudades a consultar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task<IReadOnlyList<ClimaCiudadDto>> ObtenerPorCiudadesAsync(
        IEnumerable<string> nombresCiudades,
        CancellationToken cancellationToken = default);
}
