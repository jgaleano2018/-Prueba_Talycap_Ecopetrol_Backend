/* ============================================================
   Datos de prueba para la tabla Clientes
   Base de datos: DBClientes
   ============================================================ */

USE [DBClientes];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Clientes)
BEGIN
    INSERT INTO dbo.Clientes
        (Nombre, Apellido, Email, Telefono, FechaRegistro, Identificacion)
    VALUES
        ('Juan',  'Perez',  'juan.perez@example.com',  '3001234567', '2024-01-15', '1020304050'),
        ('Maria', 'Gomez',  'maria.gomez@example.com', '3109876543', '2024-03-22', '1098765432'),
        ('Carlos','Ramirez','carlos.ramirez@example.com', NULL,       '2024-05-10', '1234567890');
END
GO
