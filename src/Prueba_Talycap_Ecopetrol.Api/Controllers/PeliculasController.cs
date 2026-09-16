using Microsoft.AspNetCore.Mvc;
using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.DTOs.Peliculas;
using Prueba_Talycap_Ecopetrol.Services.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Api.Controllers;

/// <summary>
/// Endpoints REST para la consulta de películas.
/// </summary>
[ApiController]
[Route("api/peliculas")]
[Produces("application/json")]
public class PeliculasController : ControllerBase
{
    private readonly IPeliculaService _peliculaService;
    private readonly ILogger<PeliculasController> _logger;

    public PeliculasController(IPeliculaService peliculaService, ILogger<PeliculasController> logger)
    {
        _peliculaService = peliculaService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene el listado de películas registradas de forma paginada.
    /// </summary>
    /// <param name="page">Número de página (base 0). Por defecto 0.</param>
    /// <param name="size">Cantidad de elementos por página. Por defecto 10, máximo 100.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <response code="200">Página solicitada del listado de películas.</response>
    /// <response code="500">Error interno del servidor.</response>
    [HttpGet(Name = "ObtenerTodasLasPeliculas")]
    [ProducesResponseType(typeof(PagedResultDto<PeliculaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerTodas(
        [FromQuery] int? page,
        [FromQuery] int? size,
        CancellationToken cancellationToken)
    {
        var paginacion = new ConsultaPaginadaRequestDto
        {
            Page = page ?? 0,
            Size = size ?? ConsultaPaginadaRequestDto.SizePorDefecto
        };

        try
        {
            var peliculas = await _peliculaService.ObtenerPaginadoAsync(paginacion, cancellationToken);
            return Ok(peliculas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consultar el listado de películas.");
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Mensaje = "Ocurrió un error al procesar la solicitud.",
                Detalle = ex.Message
            });
        }
    }
}
