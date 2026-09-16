using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Prueba_Talycap_Ecopetrol.DTOs.Clientes;
using Prueba_Talycap_Ecopetrol.Repositories.Context;
using Prueba_Talycap_Ecopetrol.Repositories.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de Clientes usando Entity Framework Core
/// y la ejecución del stored procedure dbo.sp_ObtenerClientePorIdentificacion.
/// </summary>
public class ClienteRepository : IClienteRepository
{
    private const string SpObtenerClientePorIdentificacion = "dbo.sp_ObtenerClientePorIdentificacion";

    private readonly ClientesDbContext _context;

    public ClienteRepository(ClientesDbContext context)
    {
        _context = context;
    }

    public ClientesDbContext Context => _context;

    /// <inheritdoc />
    public async Task<ClienteDto?> ObtenerPorIdentificacionAsync(
        string identificacion,
        CancellationToken cancellationToken = default)
    {
        // Consumo del stored procedure con un parámetro tipado.
        var parametro = new SqlParameter("@Identificacion", System.Data.SqlDbType.VarChar, 50)
        {
            Value = identificacion
        };

        var clientes = await _context.Clientes
            .FromSqlRaw($"EXEC {SpObtenerClientePorIdentificacion} @Identificacion", parametro)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var cliente = clientes.FirstOrDefault();
        if (cliente is null)
        {
            return null;
        }

        return new ClienteDto
        {
            ClienteID = cliente.ClienteID,
            Identificacion = cliente.Identificacion,
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            Email = cliente.Email,
            Telefono = cliente.Telefono,
            FechaRegistro = cliente.FechaRegistro
        };
    }

    /// <inheritdoc />
    public async Task<bool> ExisteIdentificacionAsync(
        string identificacion,
        CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .AsNoTracking()
            .AnyAsync(c => c.Identificacion == identificacion, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClienteDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .AsNoTracking()
            .OrderBy(c => c.ClienteID)
            .Select(c => new ClienteDto
            {
                ClienteID = c.ClienteID,
                Identificacion = c.Identificacion,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Email = c.Email,
                Telefono = c.Telefono,
                FechaRegistro = c.FechaRegistro
            })
            .ToListAsync(cancellationToken);
    }
}
