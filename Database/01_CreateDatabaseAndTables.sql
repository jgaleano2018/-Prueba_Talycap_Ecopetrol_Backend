/* ============================================================
   Script de creación de base de datos, tabla y procedimientos
   Proyecto: Prueba_Talycap_Ecopetrol_Backend
   Base de datos: DBClientes
   ============================================================ */

IF DB_ID('DBClientes') IS NULL
BEGIN
    CREATE DATABASE [DBClientes];
END
GO

USE [DBClientes];
GO

/* ---------- Tabla Clientes ---------- */
IF OBJECT_ID('dbo.Clientes', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Clientes](
        [ClienteID]      [int] IDENTITY(1,1) NOT NULL,
        [Nombre]         [varchar](100) NOT NULL,
        [Apellido]       [varchar](100) NOT NULL,
        [Email]          [varchar](150) NOT NULL,
        [Telefono]       [varchar](20)  NULL,
        [FechaRegistro]  [date]         NOT NULL,
        [Identificacion] [varchar](50)  NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([ClienteID] ASC)
    );

    /* Índice único para la identificación: optimiza la búsqueda del SP */
    CREATE UNIQUE NONCLUSTERED INDEX [IX_Clientes_Identificacion]
        ON [dbo].[Clientes] ([Identificacion] ASC);
END
GO
