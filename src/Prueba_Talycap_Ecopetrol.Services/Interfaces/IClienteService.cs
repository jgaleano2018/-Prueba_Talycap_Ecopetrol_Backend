using Prueba_Talycap_Ecopetrol.DTOs.Clientes;

namespace Prueba_Talycap_Ecopetrol.Services.Interfaces;

/// <summary>
/// Contrato de lógica de negocio para la gestión de clientes.
/// </summary>
public interface IClienteService
{
    /// <summary>
    /// Obtiene un cliente por su identificación.
    /// </summary>
    /// <param name="identificacion">Identificación a consultar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>El cliente encontrado o null si no existe.</returns>
    Task<ClienteDto?> ObtenerPorIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el listado completo de clientes.
    /// </summary>
    Task<IReadOnlyList<ClienteDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
