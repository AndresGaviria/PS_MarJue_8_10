CREATE DATABASE db_facturas
GO
USE db_facturas;
GO

CREATE TABLE [Peliculas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL UNIQUE,
	CONSTRAINT [PK_Peliculas] PRIMARY KEY CLUSTERED ([Id])
)
GO

INSERT INTO [Peliculas] ([Nombre]) VALUES ('Damsel');
INSERT INTO [Peliculas] ([Nombre]) VALUES ('Rescate Imposible');
INSERT INTO [Peliculas] ([Nombre]) VALUES ('La Liga de la Justicia');

CREATE TABLE [Carteleras]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Titulo] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[Fecha] SMALLDATETIME NOT NULL DEFAULT GETDATE(),
	[Pelicula] INT NOT NULL,
	[Activo] BIT NOT NULL,
	CONSTRAINT [PK_Carteleras] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Carteleras__Peliculas] FOREIGN KEY ([Pelicula]) REFERENCES [Peliculas] ([Id]) ON DELETE No Action ON UPDATE No Action
)
GO

CREATE PROCEDURE Proc_Obtener_Peliculas
AS
BEGIN
    SELECT [Id]
      ,[Nombre]
	FROM [Peliculas];
END
GO

CREATE PROCEDURE Proc_Guardar_Pelicula
	@Nombre NVARCHAR(50),
	@Response INT OUTPUT
AS
BEGIN
	INSERT INTO [Peliculas] ([Nombre]) 
	VALUES (@Nombre);

	SET @Response = 1;
END
GO

SELECT * FROM [Peliculas]
SELECT * FROM [Carteleras]
EXEC [Proc_Obtener_Peliculas]

DECLARE @respuesta INT = 0;
EXEC [Proc_Guardar_Pelicula] 'Prueba Procedimiento', @respuesta out;
SELECT @respuesta;

DELETE FROM [Peliculas] WHERE [Id] > 3;

INSERT INTO [Carteleras] ([Titulo], [Descripcion], [Fecha], [Pelicula], [Activo]) 
VALUES ('Hora estelar', 'Esta es la nueva pelicula de....', GETDATE(), 1, 1);
INSERT INTO [Carteleras] ([Titulo], [Descripcion], [Fecha], [Pelicula], [Activo]) 
VALUES ('Solo para niños', 'Ve el nuevo show de los....', GETDATE(), 2, 0);

SELECT C.[Id]
      ,C.[Titulo]
      ,C.[Descripcion]
	  ,C.[Fecha]
      ,C.[Pelicula]
      ,C.[Activo]
	  ,P.[Id]
	  ,P.[Nombre]
FROM [Carteleras] C
	INNER JOIN [Peliculas] P ON C.[Pelicula] = P.[Id];