/* ============================================================
   Stored Procedure: sp_ObtenerClientePorIdentificacion
   Base de datos: DBClientes
   ============================================================ */

USE [DBClientes];
GO

IF OBJECT_ID('dbo.sp_ObtenerClientePorIdentificacion', 'P') IS NULL
    EXEC ('CREATE PROCEDURE [dbo].[sp_ObtenerClientePorIdentificacion] AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE [dbo].[sp_ObtenerClientePorIdentificacion]
    @Identificacion VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ClienteID,
        Identificacion,
        Nombre,
        Apellido,
        Email,
        Telefono,
        FechaRegistro
    FROM Clientes
    WHERE Identificacion = @Identificacion;
END
GO
