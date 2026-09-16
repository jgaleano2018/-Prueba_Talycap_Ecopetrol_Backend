using Microsoft.EntityFrameworkCore;
using Prueba_Talycap_Ecopetrol.Repositories.Entities;

namespace Prueba_Talycap_Ecopetrol.Repositories.Context;

/// <summary>
/// Contexto de Entity Framework Core para la base de datos DBClientes.
/// </summary>
public class ClientesDbContext : DbContext
{
    public ClientesDbContext(DbContextOptions<ClientesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    public DbSet<Ciudad> Ciudades => Set<Ciudad>();

    public DbSet<RegistroClima> RegistrosClima => Set<RegistroClima>();

    public DbSet<Pelicula> Peliculas => Set<Pelicula>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes", "dbo");
            entity.HasKey(e => e.ClienteID);
            entity.Property(e => e.ClienteID).UseIdentityColumn();

            // La columna FechaRegistro es de tipo DATE en SQL Server (sin hora).
            entity.Property(e => e.FechaRegistro).HasColumnType("date");

            entity.HasIndex(e => e.Identificacion)
                  .IsUnique()
                  .HasDatabaseName("IX_Clientes_Identificacion");
        });

        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.ToTable("Ciudades", "dbo");
            entity.HasKey(e => e.CiudadID);
            entity.Property(e => e.CiudadID).UseIdentityColumn();

            entity.HasMany(e => e.RegistrosClima)
                  .WithOne(r => r.Ciudad)
                  .HasForeignKey(r => r.CiudadID)
                  .HasConstraintName("FK_RegistrosClima_Ciudades")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RegistroClima>(entity =>
        {
            entity.ToTable("RegistrosClima", "dbo");
            entity.HasKey(e => e.RegistroClimaID);
            entity.Property(e => e.RegistroClimaID).UseIdentityColumn();

            // La columna FechaConsulta se almacena como DATETIME2 en SQL Server.
            entity.Property(e => e.FechaConsulta).HasColumnType("datetime2");

            entity.HasIndex(e => e.CiudadID)
                  .HasDatabaseName("IX_RegistrosClima_CiudadID");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.ToTable("Peliculas", "dbo");
            entity.HasKey(e => e.PeliculaID);
            entity.Property(e => e.PeliculaID).UseIdentityColumn();

            // La columna FechaRegistro es de tipo DATE en SQL Server (sin hora).
            entity.Property(e => e.FechaRegistro).HasColumnType("date");

            entity.HasIndex(e => e.Titulo)
                  .HasDatabaseName("IX_Peliculas_Titulo");
        });
    }
}
