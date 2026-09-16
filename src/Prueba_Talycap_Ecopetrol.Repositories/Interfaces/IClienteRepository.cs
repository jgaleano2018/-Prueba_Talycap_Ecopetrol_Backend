using Microsoft.EntityFrameworkCore;
using Prueba_Talycap_Ecopetrol.DTOs.Clientes;
using Prueba_Talycap_Ecopetrol.Repositories.Context;

namespace Prueba_Talycap_Ecopetrol.Repositories.Interfaces;

/// <summary>
/// Contrato de acceso a datos para la tabla Clientes.
/// </summary>
public interface IClienteRepository
{
    /// <summary>
    /// Obtiene los datos de un cliente ejecutando el SP
    /// dbo.sp_ObtenerClientePorIdentificacion.
    /// </summary>
    /// <param name="identificacion">Identificación del cliente.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>El cliente encontrado o null si no existe.</returns>
    Task<ClienteDto?> ObtenerPorIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si ya existe un cliente con la identificación indicada.
    /// </summary>
    Task<bool> ExisteIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los clientes registrados.
    /// </summary>
    Task<IReadOnlyList<ClienteDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Proporciona acceso directo al DbContext para consultas con LINQ.
    /// </summary>
    ClientesDbContext Context { get; }
}
