using Microsoft.EntityFrameworkCore;
using Prueba_Talycap_Ecopetrol.DTOs.Clima;
using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.Repositories.Context;
using Prueba_Talycap_Ecopetrol.Repositories.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de Clima usando Entity Framework Core.
/// Consulta las tablas dbo.Ciudades y dbo.RegistrosClima.
/// </summary>
public class ClimaRepository : IClimaRepository
{
    private readonly ClientesDbContext _context;

    public ClimaRepository(ClientesDbContext context)
    {
        _context = context;
    }

    public ClientesDbContext Context => _context;

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClimaCiudadDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await ProyectarClima(_context.Ciudades.AsNoTracking())
            .OrderBy(c => c.Ciudad)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<PagedResultDto<ClimaCiudadDto>> ObtenerPaginadoAsync(
        ConsultaPaginadaRequestDto paginacion,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Ciudades.AsNoTracking();

        var totalRegistros = await query.CountAsync(cancellationToken);

        var items = await ProyectarClima(query)
            .OrderBy(c => c.Ciudad)
            .Skip(paginacion.Skip)
            .Take(paginacion.Size)
            .ToListAsync(cancellationToken);

        return PagedResultDto<ClimaCiudadDto>.Crear(items, totalRegistros, paginacion);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClimaCiudadDto>> ObtenerPorCiudadesAsync(
        IEnumerable<string> nombresCiudades,
        CancellationToken cancellationToken = default)
    {
        var nombres = nombresCiudades
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(n => n.Trim().ToLower())
            .Distinct()
            .ToList();

        if (nombres.Count == 0)
        {
            return await ObtenerTodosAsync(cancellationToken);
        }

        return await ProyectarClima(
                _context.Ciudades
                    .AsNoTracking()
                    .Where(c => nombres.Contains(c.Nombre.ToLower())))
            .OrderBy(c => c.Ciudad)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Proyecta cada ciudad junto con su registro de clima más reciente.
    /// </summary>
    private static IQueryable<ClimaCiudadDto> ProyectarClima(IQueryable<Entities.Ciudad> query)
    {
        return query.Select(c => new ClimaCiudadDto
        {
            CiudadID = c.CiudadID,
            Ciudad = c.Nombre,
            Pais = c.Pais,
            Latitud = c.Latitud,
            Longitud = c.Longitud,
            Temperatura = c.RegistrosClima
                .OrderByDescending(r => r.FechaConsulta)
                .Select(r => r.Temperatura)
                .FirstOrDefault(),
            SensacionTermica = c.RegistrosClima
                .OrderByDescending(r => r.FechaConsulta)
                .Select(r => r.SensacionTermica)
                .FirstOrDefault(),
            Humedad = c.RegistrosClima
                .OrderByDescending(r => r.FechaConsulta)
                .Select(r => r.Humedad)
                .FirstOrDefault(),
            VientoVelocidad = c.RegistrosClima
                .OrderByDescending(r => r.FechaConsulta)
                .Select(r => r.VientoVelocidad)
                .FirstOrDefault(),
            Condicion = c.RegistrosClima
                .OrderByDescending(r => r.FechaConsulta)
                .Select(r => r.Condicion)
                .FirstOrDefault(),
            FechaConsulta = c.RegistrosClima
                .OrderByDescending(r => r.FechaConsulta)
                .Select(r => r.FechaConsulta)
                .FirstOrDefault()
        });
    }
}
