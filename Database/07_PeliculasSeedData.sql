/* ============================================================
   Datos de prueba para la tabla Películas
   Base de datos: DBClientes
   Tablas: dbo.Peliculas
   ============================================================ */

USE [DBClientes];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Peliculas)
BEGIN
    INSERT INTO dbo.Peliculas
        (Titulo, Genero, Anio, Director, Duracion, Sinopsis, FechaRegistro)
    VALUES
        ('El Padrino',            'Drama',   1972, 'Francis Ford Coppola', 175, 'El patriarca de una dinastía del crimen organizado transfiere el control de su imperio clandestino a su hijo menor.', '2024-06-01'),
        ('Pulp Fiction',          'Crimen',  1994, 'Quentin Tarantino',    154, 'Las vidas de dos sicarios, un boxeador y la esposa de un gánster se entrelazan en cuatro relatos de violencia y redención.', '2024-06-01'),
        ('El Señor de los Anillos: La Comunidad del Anillo', 'Fantasía', 2001, 'Peter Jackson', 178, 'Un hobbit emprende un viaje para destruir un anillo poderoso y evitar que caiga en manos del señor oscuro Sauron.', '2024-06-01'),
        ('Inception',             'Ciencia ficción', 2010, 'Christopher Nolan', 148, 'Un ladrón que roba secretos a través de la tecnología de compartir sueños recibe la misión inversa: implantar una idea.', '2024-06-01'),
        ('Coco',                  'Animación', 2017, 'Lee Unkrich',         105, 'Un niño aspirante a músico es transportado a la Tierra de los Muertos, donde busca el secreto de su historia familiar.', '2024-06-01');
END
GO
