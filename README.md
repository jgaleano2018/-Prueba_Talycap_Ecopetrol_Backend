# Documentación técnica — Backend (.NET 10) . Resumen

## 1. Proyecto backend basado en .NET 10 (solicitado).
Solución (evidencia): Prueba_Talycap_Ecopetrol_Backend.slnx incluye los proyectos:
Prueba_Talycap_Ecopetrol.Api
Prueba_Talycap_Ecopetrol.DTOs
Prueba_Talycap_Ecopetrol.Repositories
Prueba_Talycap_Ecopetrol.Services

## 2. Tecnologías
Plataforma: .NET 10 (SDK .NET 10).
Lenguaje: C# (versión acorde al SDK).
Dependencias típicas recomendadas:
ASP.NET Core Web API
Entity Framework Core (si aplica)
AutoMapper (opcional para DTOs)
FluentValidation / DataAnnotations (validación)
xUnit / NUnit / MSTest (pruebas unitarias)
Moq / NSubstitute (mocks)
Serilog / Microsoft.Extensions.Logging (logging)
Swagger / Swashbuckle (documentación OpenAPI)

## 3. Estructura del proyecto (según solución)
/src/Prueba_Talycap_Ecopetrol.Api
Proyecto API — controladores, middleware, Program.cs
/src/Prueba_Talycap_Ecopetrol.DTOs
Contratos entre capas (DTOs)
/src/Prueba_Talycap_Ecopetrol.Repositories
Acceso a datos (repositorios, contextos DB)
/src/Prueba_Talycap_Ecopetrol.Services
Lógica de negocio y orquestación entre repos y API
(Evidencia: listado de proyectos extraído del archivo .slnx adjunto)

## 4. Requisitos previos
Instalar .NET 10 SDK (descargar desde dotnet.microsoft.com si no lo tienes).
(Si se usa DB) Motor de base de datos requerido (SQL Server / PostgreSQL / etc.) y credenciales.
Herramientas opcionales:
dotnet-ef (si se usan migrations): dotnet tool install --global dotnet-ef
Postman / HTTP client para probar endpoints
Docker (si se desea contenerizar)
Variables de entorno / secrets:
connection string (ej.: ConnectionStrings__Default)
configuración JWT / Identity (si aplica)

## 5. Puesta en marcha (desarrollo local)
Clonar el repositorio:
git clone <repo-url>
Construir la solución:
dotnet build Prueba_Talycap_Ecopetrol_Backend.slnx
(o) dotnet build src/Prueba_Talycap_Ecopetrol.Api/Prueba_Talycap_Ecopetrol.Api.csproj
Configurar appsettings:
Copia appsettings.Development.json.example → appsettings.Development.json y rellena connection strings y secrets.
Ejecutar la API:
dotnet run --project src/Prueba_Talycap_Ecopetrol.Api
La API por defecto escuchará en los puertos configurados en launchSettings o en appsettings; por ejemplo: https://localhost:5001
Migraciones (si aplica):
dotnet ef migrations add Initial --project src/Prueba_Talycap_Ecopetrol.Repositories --startup-project src/Prueba_Talycap_Ecopetrol.Api
dotnet ef database update --project src/Prueba_Talycap_Ecopetrol.Repositories --startup-project src/Prueba_Talycap_Ecopetrol.Api

## 6. Arquitectura del backend
Capas:
API (presentación): controladores que exponen endpoints HTTP y validan input.
Services (negocio): orquestan reglas de negocio y transacciones.
Repositories (persistencia): encapsulan acceso a la base de datos (EF Core / Dapper).
DTOs: objetos de transferencia para desacoplar entidades de dominio de la capa externa.
Principios aplicados recomendados:
Inyección de dependencias (IServiceCollection) en Program.cs.
Separación de responsabilidades (SRP): controllers delgados, lógica en Services.
Repositorios unit testables e interfaces (IRepository).
Tratamiento centralizado de errores (middleware de excepción).
Logging estructurado y correlación de peticiones (traceId).

## 7. Endpoints expuestos
Nota: no se pudieron inferir los controladores/acciones del código suministrado. A continuación plantilla de ejemplo y formato para documentar tus endpoints; pega aquí los controladores o el resultado de swagger para que lo llene automáticamente.
Ejemplo de formato (completa según tus controladores):

GET /api/values
Descripción: devuelve lista de valores.
Query params: ?page=1&size=20
Respuesta 200: [{ id: int, name: string }, ...]
GET /api/items/{id}
Descripción: devuelve item por id
Respuesta 200: { id, name, ... } | 404
POST /api/items
Descripción: crea item
Body: { name: string, ... }
Respuesta 201: ubicación del recurso
Cómo generar automáticamente:

Si Swashbuckle/Swagger está habilitado, abre /swagger/index.html en la app en ejecución y exporta la lista de endpoints.
Pega aquí el listado de controladores (o el Program.cs / Controllers) y lo incorporo con ejemplos y modelos de request/response.
Test unitarios
Framework recomendado: xUnit (configurable).
Ubicación de tests: /tests/ (o un proyecto tests por cada capa).
Comandos:
dotnet test --no-build --verbosity normal
Para cobertura, usar coverlet o herramientas específicas (ej. dotnet test /p:CollectCoverage=true con coverlet).
Plantilla de test simple (xUnit + Moq):
Arrange: configurar mocks del repositorio
Act: invocar método del Service/Controller
Assert: verificar resultado y llamadas a mocks
Ejemplo:

csharp


[Fact]
public async Task GetItem_Returns_Item_WhenExists()
{
    // Arrange
    var repoMock = new Mock<IItemRepository>();
    repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Item { Id = 1, Name = "X" });
    var svc = new ItemService(repoMock.Object);

    // Act
    var result = await svc.GetByIdAsync(1);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(1, result.Id);
}


## 8. CI/CD: ejemplo GitHub Actions
A continuación un workflow de ejemplo que:
Usa .NET 10 SDK
Restaura, build, test y publica artifacts
Ejecuta verificación de seguridad básica (dotnet format / analyzers opcional)
.github/workflows/ci.yml (ejemplo)