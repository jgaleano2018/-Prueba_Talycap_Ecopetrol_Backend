/* ============================================================
   Script de creación de tablas de Clima
   Proyecto: Prueba_Talycap_Ecopetrol_Backend
   Base de datos: DBClientes
   Tablas: dbo.Ciudades, dbo.RegistrosClima
   ============================================================ */

USE [DBClientes];
GO

/* ---------- Tabla Ciudades ---------- */
IF OBJECT_ID('dbo.Ciudades', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Ciudades](
        [CiudadID] [int] IDENTITY(1,1) NOT NULL,
        [Nombre]   [varchar](100) NOT NULL,
        [Pais]     [varchar](100) NOT NULL,
        [Latitud]  [float] NULL,
        [Longitud] [float] NULL,
        CONSTRAINT [PK_Ciudades] PRIMARY KEY CLUSTERED ([CiudadID] ASC)
    );
END
GO

/* ---------- Tabla RegistrosClima ---------- */
IF OBJECT_ID('dbo.RegistrosClima', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[RegistrosClima](
        [RegistroClimaID]  [int] IDENTITY(1,1) NOT NULL,
        [CiudadID]         [int] NOT NULL,
        [Temperatura]      [float] NOT NULL,
        [SensacionTermica] [float] NOT NULL,
        [Humedad]          [int]   NOT NULL,
        [VientoVelocidad]  [float] NOT NULL,
        [Condicion]        [varchar](20) NULL,
        [FechaConsulta]    [datetime2] NOT NULL,
        CONSTRAINT [PK_RegistrosClima] PRIMARY KEY CLUSTERED ([RegistroClimaID] ASC),
        CONSTRAINT [FK_RegistrosClima_Ciudades]
            FOREIGN KEY ([CiudadID]) REFERENCES [dbo].[Ciudades]([CiudadID])
    );

    CREATE NONCLUSTERED INDEX [IX_RegistrosClima_CiudadID]
        ON [dbo].[RegistrosClima] ([CiudadID] ASC);
END
GO
