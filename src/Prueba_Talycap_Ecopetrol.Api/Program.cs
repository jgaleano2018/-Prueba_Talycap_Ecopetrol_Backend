using Prueba_Talycap_Ecopetrol.Api.Extensions;
using Prueba_Talycap_Ecopetrol.Repositories;
using Prueba_Talycap_Ecopetrol.Services;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// Controladores
// ---------------------------------------------------------------
builder.Services.AddControllers();

// ---------------------------------------------------------------
// Swagger / OpenAPI
// ---------------------------------------------------------------
builder.Services.AddSwaggerDocumentation();

// ---------------------------------------------------------------
// CORS (permite consumir la API desde un front-end)
// ---------------------------------------------------------------
// Orígenes permitidos para consumir la API (front-end Angular en local).
var origenesPermitidos = new[]
{
    "http://localhost:4200"
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
        policy.WithOrigins(origenesPermitidos)
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ---------------------------------------------------------------
// Inyección de dependencias por capas
// Repositories (EF Core + SqlServer) -> Services -> API
// ---------------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DBClientes")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'DBClientes' en appsettings.json.");

builder.Services.AddRepositories(connectionString);
builder.Services.AddServices();

var app = builder.Build();

// ---------------------------------------------------------------
// Pipeline HTTP
// ---------------------------------------------------------------
// Swagger habilitado siempre (útil para la prueba técnica).
app.UseSwaggerDocumentation();

app.UseCors("PermitirFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Redirige la raíz a la interfaz de Swagger.
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
