using Microsoft.AspNetCore.Mvc;
using Prueba_Talycap_Ecopetrol.DTOs.Clima;
using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.Services.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Api.Controllers;

/// <summary>
/// Endpoints REST para la consulta del clima de varias ciudades.
/// </summary>
[ApiController]
[Route("api/clima")]
[Produces("application/json")]
public class ClimaController : ControllerBase
{
    private readonly IClimaService _climaService;
    private readonly ILogger<ClimaController> _logger;

    public ClimaController(IClimaService climaService, ILogger<ClimaController> logger)
    {
        _climaService = climaService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene el clima de las ciudades registradas de forma paginada.
    /// </summary>
    /// <param name="page">Número de página (base 0). Por defecto 0.</param>
    /// <param name="size">Cantidad de elementos por página. Por defecto 10, máximo 100.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <response code="200">Página solicitada del listado de clima por ciudad.</response>
    /// <response code="500">Error interno del servidor.</response>
    [HttpGet(Name = "ObtenerClimaTodasLasCiudades")]
    [ProducesResponseType(typeof(PagedResultDto<ClimaCiudadDto>), StatusCodes.Status200OK)]
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
            var clima = await _climaService.ObtenerPaginadoAsync(paginacion, cancellationToken);
            return Ok(clima);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consultar el clima de todas las ciudades.");
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Mensaje = "Ocurrió un error al procesar la solicitud.",
                Detalle = ex.Message
            });
        }
    }

    /// <summary>
    /// Obtiene el clima de varias ciudades indicadas por nombre.
    /// </summary>
    /// <param name="ciudades">
    /// Nombres de las ciudades separados por coma (por ejemplo: "Bogotá,Medellín").
    /// Si se omite, se devuelven todas las ciudades registradas.
    /// </param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <response code="200">Listado de clima de las ciudades solicitadas.</response>
    /// <response code="400">No se enviaron ciudades válidas para consultar.</response>
    /// <response code="500">Error interno del servidor.</response>
    [HttpGet("ciudades", Name = "ObtenerClimaPorCiudades")]
    [ProducesResponseType(typeof(IReadOnlyList<ClimaCiudadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerPorCiudades(
        [FromQuery] string? ciudades,
        CancellationToken cancellationToken)
    {
        try
        {
            var nombres = (ciudades ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var clima = await _climaService.ObtenerPorCiudadesAsync(nombres, cancellationToken);

            if (clima.Count == 0)
            {
                return NotFound(new ErrorResponseDto
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensaje = "No se encontró información de clima para las ciudades indicadas."
                });
            }

            return Ok(clima);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consultar el clima de las ciudades {Ciudades}.", ciudades);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Mensaje = "Ocurrió un error al procesar la solicitud.",
                Detalle = ex.Message
            });
        }
    }
}
