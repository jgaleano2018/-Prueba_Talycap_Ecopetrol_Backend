using Prueba_Talycap_Ecopetrol.DTOs.Clientes;
using Prueba_Talycap_Ecopetrol.Repositories.Interfaces;
using Prueba_Talycap_Ecopetrol.Services.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Services.Implementations;

/// <summary>
/// Lógica de negocio de clientes. Delega el acceso a datos en el repositorio.
/// </summary>
public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    /// <inheritdoc />
    public async Task<ClienteDto?> ObtenerPorIdentificacionAsync(
        string identificacion,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(identificacion))
        {
            throw new ArgumentException("La identificación es obligatoria.", nameof(identificacion));
        }

        var identificacionNormalizada = identificacion.Trim();

        return await _clienteRepository.ObtenerPorIdentificacionAsync(
            identificacionNormalizada,
            cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClienteDto>> ObtenerTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _clienteRepository.ObtenerTodosAsync(cancellationToken);
    }
}
