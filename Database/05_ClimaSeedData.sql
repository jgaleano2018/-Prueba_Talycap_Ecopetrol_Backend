/* ============================================================
   Datos de prueba para las tablas de Clima
   Base de datos: DBClientes
   Tablas: dbo.Ciudades, dbo.RegistrosClima
   ============================================================ */

USE [DBClientes];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Ciudades)
BEGIN
    INSERT INTO dbo.Ciudades (Nombre, Pais, Latitud, Longitud)
    VALUES
        ('Bogota',   'Colombia', 4.7110,  -74.0721),
        ('Medellin', 'Colombia', 6.2442,  -75.5812),
        ('Cali',     'Colombia', 3.4516,  -76.5320),
        ('Barranquilla', 'Colombia', 10.9685, -74.7813);
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.RegistrosClima)
BEGIN
    INSERT INTO dbo.RegistrosClima
        (CiudadID, Temperatura, SensacionTermica, Humedad, VientoVelocidad, Condicion, FechaConsulta)
    SELECT c.CiudadID, 19.5, 19.0, 78, 3.4, 'Nublado', '2024-06-01T08:00:00'
    FROM dbo.Ciudades c WHERE c.Nombre = 'Bogota';

    INSERT INTO dbo.RegistrosClima
        (CiudadID, Temperatura, SensacionTermica, Humedad, VientoVelocidad, Condicion, FechaConsulta)
    SELECT c.CiudadID, 24.8, 25.5, 65, 2.1, 'Parcialmente nublado', '2024-06-01T08:00:00'
    FROM dbo.Ciudades c WHERE c.Nombre = 'Medellin';

    INSERT INTO dbo.RegistrosClima
        (CiudadID, Temperatura, SensacionTermica, Humedad, VientoVelocidad, Condicion, FechaConsulta)
    SELECT c.CiudadID, 28.3, 31.0, 70, 1.5, 'Soleado', '2024-06-01T08:00:00'
    FROM dbo.Ciudades c WHERE c.Nombre = 'Cali';

    INSERT INTO dbo.RegistrosClima
        (CiudadID, Temperatura, SensacionTermica, Humedad, VientoVelocidad, Condicion, FechaConsulta)
    SELECT c.CiudadID, 30.1, 34.2, 74, 5.6, 'Soleado', '2024-06-01T08:00:00'
    FROM dbo.Ciudades c WHERE c.Nombre = 'Barranquilla';
END
GO
