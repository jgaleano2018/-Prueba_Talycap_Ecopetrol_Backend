using Microsoft.EntityFrameworkCore;
using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.DTOs.Peliculas;
using Prueba_Talycap_Ecopetrol.Repositories.Context;
using Prueba_Talycap_Ecopetrol.Repositories.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de Películas usando Entity Framework Core.
/// Consulta la tabla dbo.Peliculas.
/// </summary>
public class PeliculaRepository : IPeliculaRepository
{
    private readonly ClientesDbContext _context;

    public PeliculaRepository(ClientesDbContext context)
    {
        _context = context;
    }

    public ClientesDbContext Context => _context;

    /// <inheritdoc />
    public async Task<IReadOnlyList<PeliculaDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await Proyectar(_context.Peliculas.AsNoTracking())
            .OrderBy(p => p.Titulo)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<PagedResultDto<PeliculaDto>> ObtenerPaginadoAsync(
        ConsultaPaginadaRequestDto paginacion,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Peliculas.AsNoTracking();

        var totalRegistros = await query.CountAsync(cancellationToken);

        var items = await Proyectar(query)
            .OrderBy(p => p.PeliculaID)
            .Skip(paginacion.Skip)
            .Take(paginacion.Size)
            .ToListAsync(cancellationToken);

        return PagedResultDto<PeliculaDto>.Crear(items, totalRegistros, paginacion);
    }

    /// <summary>
    /// Proyecta las entidades de películas a su DTO de salida.
    /// </summary>
    private static IQueryable<PeliculaDto> Proyectar(IQueryable<Entities.Pelicula> query)
    {
        return query.Select(p => new PeliculaDto
        {
            PeliculaID = p.PeliculaID,
            Titulo = p.Titulo,
            Genero = p.Genero,
            Anio = p.Anio,
            Director = p.Director,
            Duracion = p.Duracion,
            Sinopsis = p.Sinopsis,
            FechaRegistro = p.FechaRegistro
        });
    }
}
