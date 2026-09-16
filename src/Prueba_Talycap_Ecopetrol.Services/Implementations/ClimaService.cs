using Prueba_Talycap_Ecopetrol.DTOs.Clima;
using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.Repositories.Interfaces;
using Prueba_Talycap_Ecopetrol.Services.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Services.Implementations;

/// <summary>
/// Lógica de negocio del clima. Delega el acceso a datos en el repositorio.
/// </summary>
public class ClimaService : IClimaService
{
    private readonly IClimaRepository _climaRepository;

    public ClimaService(IClimaRepository climaRepository)
    {
        _climaRepository = climaRepository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClimaCiudadDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _climaRepository.ObtenerTodosAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<PagedResultDto<ClimaCiudadDto>> ObtenerPaginadoAsync(
        ConsultaPaginadaRequestDto paginacion,
        CancellationToken cancellationToken = default)
    {
        return await _climaRepository.ObtenerPaginadoAsync(paginacion, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClimaCiudadDto>> ObtenerPorCiudadesAsync(
        IEnumerable<string> nombresCiudades,
        CancellationToken cancellationToken = default)
    {
        var nombres = (nombresCiudades ?? Enumerable.Empty<string>())
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(n => n.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        return await _climaRepository.ObtenerPorCiudadesAsync(nombres, cancellationToken);
    }
}
