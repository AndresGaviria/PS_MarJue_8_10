
CREATE DATABASE db_hopitales
GO
USE db_hopitales;
GO

CREATE TABLE [Personas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Cedula] NVARCHAR(11) NOT NULL UNIQUE,
	[Nombre] NVARCHAR(150) NOT NULL,
	CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED ([Id])
)
GO

INSERT INTO [Personas] ([Cedula], [Nombre]) VALUES ('203', 'Juan');
INSERT INTO [Personas] ([Cedula], [Nombre]) VALUES ('204', 'Pepito');
INSERT INTO [Personas] ([Cedula], [Nombre]) VALUES ('205', 'Daniela');
INSERT INTO [Personas] ([Cedula], [Nombre]) VALUES ('206', 'Jorges');

SELECT * FROM [Personas];

UPDATE [Personas]
SET [Nombre] = 'Jorge'
WHERE [Cedula] = '206';

INSERT INTO [Personas] ([Cedula], [Nombre]) VALUES ('207', 'Pruebas');

DELETE FROM [Personas]
WHERE [Id] = 5;


CREATE TABLE [Doctores]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Codigo] NVARCHAR(11) NOT NULL UNIQUE,
	[Nombre] NVARCHAR(150) NOT NULL,
	CONSTRAINT [PK_Doctores] PRIMARY KEY CLUSTERED ([Id])
)
GO

INSERT INTO [Doctores] ([Codigo], [Nombre]) VALUES ('501', 'Alex');
INSERT INTO [Doctores] ([Codigo], [Nombre]) VALUES ('502', 'Rodolfo');
INSERT INTO [Doctores] ([Codigo], [Nombre]) VALUES ('503', 'Juan');
INSERT INTO [Doctores] ([Codigo], [Nombre]) VALUES ('504', 'Luisa');

CREATE TABLE [Citas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Fecha] SMALLDATETIME NOT NULL,
	[Persona] INT NOT NULL,
	[Doctor] INT NOT NULL,
	[Descripcion] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [PK_Citas] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Citas__Persona] FOREIGN KEY ([Persona]) REFERENCES [Personas] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Citas__Doctor] FOREIGN KEY ([Doctor]) REFERENCES [Doctores] ([Id]) ON DELETE No Action ON UPDATE No Action
)
GO

INSERT INTO [Citas] ([Fecha], [Persona], [Doctor], [Descripcion]) VALUES (GETDATE(), 1, 1, 'COVID...UCI');
INSERT INTO [Citas] ([Fecha], [Persona], [Doctor], [Descripcion]) VALUES (GETDATE(), 2, 3, 'TRAU....RAD...ACET..');
INSERT INTO [Citas] ([Fecha], [Persona], [Doctor], [Descripcion]) VALUES (GETDATE(), 3, 2, 'EMB..TRAM');

SELECT C.[Id], 
	C.[Fecha], 
	C.[Doctor], 
	C.[Descripcion],
	P.[Id] 'P_Id',
	P.[Cedula] 'P_Cedula',
	P.[Nombre] 'P_Nombre',
	D.[Id] 'D_Id',
	D.[Codigo] 'D_Codigo',
	D.[Nombre] 'D_Nombre'
INTO Temporal
FROM [Citas] AS C 
	INNER JOIN [Personas] P ON P.Id = C.Persona
	INNER JOIN [Doctores] D ON D.id = C.Doctor;
GO


SELECT * FROM Temporal;

SELECT * 
INTO Personas1
FROM Personas;

SELECT *
FROM Personas1;

DELETE FROM Personas1
WHERE id = 2;


DROP TABLE personas1;

UPDATE [Personas1]
SET [Nombre] = 'Nacho'
WHERE [id] = 1;