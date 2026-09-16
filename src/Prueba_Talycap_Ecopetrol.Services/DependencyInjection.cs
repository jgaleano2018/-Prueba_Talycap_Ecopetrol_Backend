using Microsoft.Extensions.DependencyInjection;
using Prueba_Talycap_Ecopetrol.Services.Implementations;
using Prueba_Talycap_Ecopetrol.Services.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Services;

/// <summary>
/// Registro de dependencias de la capa de lógica de negocio.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IClimaService, ClimaService>();
        services.AddScoped<IPeliculaService, PeliculaService>();
        return services;
    }
}
