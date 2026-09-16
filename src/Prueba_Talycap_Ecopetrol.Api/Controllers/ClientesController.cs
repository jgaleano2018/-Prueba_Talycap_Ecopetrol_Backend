using Microsoft.AspNetCore.Mvc;
using Prueba_Talycap_Ecopetrol.DTOs.Clientes;
using Prueba_Talycap_Ecopetrol.DTOs.Comunes;
using Prueba_Talycap_Ecopetrol.Services.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Api.Controllers;

/// <summary>
/// Endpoints REST para la gestión de clientes.
/// </summary>
[ApiController]
[Route("api/clientes")]
[Produces("application/json")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly ILogger<ClientesController> _logger;

    public ClientesController(IClienteService clienteService, ILogger<ClientesController> logger)
    {
        _clienteService = clienteService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene los datos de un cliente a partir de su identificación.
    /// Consume el stored procedure dbo.sp_ObtenerClientePorIdentificacion.
    /// </summary>
    /// <param name="identificacion">Identificación del cliente a consultar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <response code="200">Cliente encontrado.</response>
    /// <response code="400">La identificación enviada no es válida.</response>
    /// <response code="404">No existe un cliente con la identificación indicada.</response>
    /// <response code="500">Error interno del servidor.</response>
    [HttpGet("{identificacion}", Name = "ObtenerClientePorIdentificacion")]
    [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerPorIdentificacion(
        [FromRoute] string identificacion,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(identificacion))
        {
            return BadRequest(new ErrorResponseDto
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Mensaje = "La identificación es obligatoria."
            });
        }

        try
        {
            var cliente = await _clienteService.ObtenerPorIdentificacionAsync(
                identificacion,
                cancellationToken);

            if (cliente is null)
            {
                return NotFound(new ErrorResponseDto
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensaje = $"No se encontró un cliente con la identificación '{identificacion}'."
                });
            }

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error al consultar el cliente con identificación {Identificacion}.",
                identificacion);

            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Mensaje = "Ocurrió un error al procesar la solicitud.",
                Detalle = ex.Message
            });
        }
    }

    /// <summary>
    /// Obtiene el listado completo de clientes registrados.
    /// </summary>
    /// <response code="200">Listado de clientes.</response>
    [HttpGet(Name = "ObtenerTodosLosClientes")]
    [ProducesResponseType(typeof(IReadOnlyList<ClienteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var clientes = await _clienteService.ObtenerTodosAsync(cancellationToken);
        return Ok(clientes);
    }
}
