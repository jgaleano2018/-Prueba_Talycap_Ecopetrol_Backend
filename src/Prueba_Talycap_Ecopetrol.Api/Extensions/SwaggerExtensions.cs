using Microsoft.OpenApi;
using System.Reflection;

namespace Prueba_Talycap_Ecopetrol.Api.Extensions;

/// <summary>
/// Configuración del servicio de Swagger (Swashbuckle) y de la
/// documentación OpenAPI de la API.
/// </summary>
public static class SwaggerExtensions
{
    private const string NombreDocumento = "v1";
    private const string Titulo = "Prueba Talycap Ecopetrol - API de Clientes y Clima";
    private const string Descripcion =
        "API REST sobre la base de datos DBClientes. " +
        "Expone la consulta de clientes (incluyendo la búsqueda por identificación con el stored procedure " +
        "dbo.sp_ObtenerClientePorIdentificacion) y la consulta del clima de varias ciudades.";

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(NombreDocumento, new OpenApiInfo
            {
                Title = Titulo,
                Version = NombreDocumento,
                Description = Descripcion,
                Contact = new OpenApiContact
                {
                    Name = "Prueba Técnica Talycap - Ecopetrol"
                }
            });

            // Incluye los comentarios XML del proyecto API en la documentación.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            }
        });

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{NombreDocumento}/swagger.json", $"{Titulo} {NombreDocumento}");
            options.RoutePrefix = "swagger";
            options.DocumentTitle = Titulo;
        });

        return app;
    }
}
