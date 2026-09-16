using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prueba_Talycap_Ecopetrol.Repositories.Context;
using Prueba_Talycap_Ecopetrol.Repositories.Implementations;
using Prueba_Talycap_Ecopetrol.Repositories.Interfaces;

namespace Prueba_Talycap_Ecopetrol.Repositories;

/// <summary>
/// Registro de dependencias de la capa de acceso a datos.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ClientesDbContext>(options =>
            options.UseSqlServer(connectionString, sql =>
            {
                sql.MigrationsAssembly(typeof(ClientesDbContext).Assembly.FullName);
                sql.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
                sql.CommandTimeout(30);
            }));

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClimaRepository, ClimaRepository>();
        services.AddScoped<IPeliculaRepository, PeliculaRepository>();

        return services;
    }
}
