/* ============================================================
   Script de creación de la tabla de Películas
   Proyecto: Prueba_Talycap_Ecopetrol_Backend
   Base de datos: DBClientes
   Tablas: dbo.Peliculas
   ============================================================ */

USE [DBClientes];
GO

/* ---------- Tabla Peliculas ---------- */
IF OBJECT_ID('dbo.Peliculas', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Peliculas](
        [PeliculaID]  [int] IDENTITY(1,1) NOT NULL,
        [Titulo]      [varchar](200) NOT NULL,
        [Genero]      [varchar](100) NULL,
        [Anio]        [int]          NULL,
        [Director]    [varchar](150) NULL,
        [Duracion]    [int]          NULL,
        [Sinopsis]    [varchar](500) NULL,
        [FechaRegistro] [date]       NOT NULL,
        CONSTRAINT [PK_Peliculas] PRIMARY KEY CLUSTERED ([PeliculaID] ASC)
    );

    /* Índice para optimizar las búsquedas por título */
    CREATE NONCLUSTERED INDEX [IX_Peliculas_Titulo]
        ON [dbo].[Peliculas] ([Titulo] ASC);
END
GO
