CREATE DATABASE db_bancos
GO
USE db_bancos;
GO

CREATE TABLE [Personas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Cedula] NVARCHAR(11) NOT NULL UNIQUE,
	[Nombre] NVARCHAR(150) NOT NULL,
	[Fondos] DECIMAL(10, 2) NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED ([Id])
)
GO

INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos]) 
VALUES ('203', 'Edgar', 2000000);
INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos]) 
VALUES ('205', 'Juanita', 50);
INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos]) 
VALUES ('206', 'Yesenia', 200000);
INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos]) 
VALUES ('204', 'Julian', 100000);
INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos]) 
VALUES ('208', 'Pablo', 50000);
INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos]) 
VALUES ('209', 'Pepitos', 0);
INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos]) 
VALUES ('207', 'Pedro', 100000);


SELECT * FROM [Personas];

UPDATE [Personas] 
SET [Nombre] = 'Pepito'
WHERE [Id] = 6;

INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos])
VALUES('210','sara', '1000000');
INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos])
VALUES('211','Pruebas', 1000000);
INSERT INTO [Personas] ([Cedula], [Nombre], [Fondos])
VALUES('212','Test', 0);

DELETE FROM [Personas] WHERE [Id] = 10 OR [Id] = 11;  
DELETE FROM [Personas] WHERE [Id] IN (10, 11);  

CREATE TABLE [Movimientos]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Fecha] SMALLDATETIME NOT NULL,
	[De] INT NOT NULL,
	[Tipo] NVARCHAR(50) NOT NULL,
	[Valor] DECIMAL(10, 2) NOT NULL,
	CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Movimientos__Personas] FOREIGN KEY ([De]) REFERENCES [Personas] ([Id]) ON DELETE No Action ON UPDATE No Action
)
GO

INSERT INTO [Movimientos] ([Fecha],[De],[Tipo],[Valor])
VALUES(GETDATE(), 1, 'Trans..', 200000);
INSERT INTO [Movimientos] ([Fecha],[De],[Tipo],[Valor])
VALUES(GETDATE(), 4, 'Trans..', 150000);
INSERT INTO [Movimientos] ([Fecha],[De],[Tipo],[Valor])
VALUES(GETDATE(), 2, 'Retir..', 3000000);

SELECT * FROM [Movimientos];

UPDATE [Movimientos] 
SET [Valor] = 150000
WHERE [Id] = 3;


CREATE TABLE [Detalles]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Movimiento] INT NOT NULL,
	[Para] INT NOT NULL,
	[Valor] DECIMAL(10, 2) NOT NULL,
	CONSTRAINT [PK_Detalles] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Detalles__Movimientos] FOREIGN KEY ([Movimiento]) REFERENCES [Movimientos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Detalles__Personas] FOREIGN KEY ([Para]) REFERENCES [Personas] ([Id]) ON DELETE No Action ON UPDATE No Action
)
GO

SELECT * FROM [Movimientos];
SELECT * FROM [Personas];

INSERT INTO [Detalles] ([Movimiento], [Para], [Valor])
VALUES(1, 3, 200000);
INSERT INTO [Detalles] ([Movimiento], [Para], [Valor])
VALUES(3, 7, 100000);
INSERT INTO [Detalles] ([Movimiento], [Para], [Valor])
VALUES(3, 5, 50000);

SELECT * FROM [Detalles];

SELECT *
INTO [Temporal] 
FROM [Personas];

SELECT *
FROM [Temporal];

SELECT * FROM [Personas] P INNER JOIN [Movimientos] M ON M.De = P.Id 