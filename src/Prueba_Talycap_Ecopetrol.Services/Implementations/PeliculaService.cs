using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.DTOs.Peliculas;
using Prueba_Talycap_Ecopetrol.Repositories.Interfaces;
using Prueba_Talycap_Ecopetrol.Services.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Services.Implementations;

/// <summary>
/// Lógica de negocio de películas. Delega el acceso a datos en el repositorio.
/// </summary>
public class PeliculaService : IPeliculaService
{
    private readonly IPeliculaRepository _peliculaRepository;

    public PeliculaService(IPeliculaRepository peliculaRepository)
    {
        _peliculaRepository = peliculaRepository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<PeliculaDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _peliculaRepository.ObtenerTodosAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<PagedResultDto<PeliculaDto>> ObtenerPaginadoAsync(
        ConsultaPaginadaRequestDto paginacion,
        CancellationToken cancellationToken = default)
    {
        return await _peliculaRepository.ObtenerPaginadoAsync(paginacion, cancellationToken);
    }
}
