
CREATE DATABASE db_facturas
GO
USE db_facturas;
GO

CREATE TABLE [Productos]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Precio] DECIMAL(10,2) NOT NULL,
	[Cantidad] DECIMAL(10,2) NOT NULL,
	[Iva] DECIMAL(10,2) NOT NULL,
	CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED ([Id])
)
GO

CREATE TABLE [MetodoDePagos]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Tipo] INT NOT NULL,
	CONSTRAINT [PK_MetodoDePagos] PRIMARY KEY CLUSTERED ([Id])
)
GO

CREATE TABLE [Personas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Cedula] NVARCHAR(50) NOT NULL,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Fecha] SMALLDATETIME NOT NULL,
	[Activo] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED ([Id])
)
GO

CREATE TABLE [Facturas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Numero] NVARCHAR(15) NOT NULL,
	[Fecha] SMALLDATETIME NOT NULL,
	[Persona] INT NOT NULL,
	[MetodoDePago] INT NOT NULL,
	[Iva] DECIMAL(10,2) NOT NULL,
	[Total] DECIMAL(10,2) NOT NULL,
	CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Facturas__Personas] FOREIGN KEY ([Persona]) REFERENCES [Personas] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Facturas__MetodoDePagos] FOREIGN KEY ([MetodoDePago]) REFERENCES [MetodoDePagos] ([Id]) ON DELETE No Action ON UPDATE No Action
)
GO

CREATE TABLE [Detalles]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Factura] INT NOT NULL,
	[Producto] INT NOT NULL,
	[Precio] DECIMAL(10,2) NOT NULL,
	[Cantidad] DECIMAL(10,2) NOT NULL,
	[Iva] DECIMAL(10,2) NOT NULL,
	[Total] DECIMAL(10,2) NOT NULL,
	CONSTRAINT [PK_Detalles] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Detalles__Facturas] FOREIGN KEY ([Factura]) REFERENCES [Facturas] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Detalles__Productos] FOREIGN KEY ([Producto]) REFERENCES [Productos] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO
   
IF NOT EXISTS (SELECT 1 FROM [MetodoDePagos] WHERE [Nombre] = 'Efectivo')
BEGIN
	INSERT INTO [MetodoDePagos] ([Nombre], [Tipo])
	VALUES ('Efectivo', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [MetodoDePagos] WHERE [Nombre] = 'Tarjeta')
BEGIN
	INSERT INTO [MetodoDePagos] ([Nombre], [Tipo])
	VALUES ('Tarjeta', 1);
END 

IF NOT EXISTS (SELECT 1 FROM [MetodoDePagos] WHERE [Nombre] = 'Transferencia')
BEGIN
	INSERT INTO [MetodoDePagos] ([Nombre], [Tipo])
	VALUES ('Transferencia', 1);
END 

IF NOT EXISTS (SELECT 1 FROM [Productos] WHERE [Nombre] = 'Tv')
BEGIN
	INSERT INTO [Productos] ([Nombre],[Precio],[Cantidad],[Iva])
	VALUES ('Tv', 2000000.0, 5, 1);
END 

IF NOT EXISTS (SELECT 1 FROM [Productos] WHERE [Nombre] = 'Aguacate')
BEGIN
	INSERT INTO [Productos] ([Nombre],[Precio],[Cantidad],[Iva])
	VALUES ('Aguacate', 2000.0, 20, 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Productos] WHERE [Nombre] = 'Celular')
BEGIN
	INSERT INTO [Productos] ([Nombre],[Precio],[Cantidad],[Iva])
	VALUES ('Celular', 1500000.0, 10, 1);
END 

IF NOT EXISTS (SELECT 1 FROM [Productos] WHERE [Nombre] = 'Estencion')
BEGIN
	INSERT INTO [Productos] ([Nombre],[Precio],[Cantidad],[Iva])
	VALUES ('Estencion', 20000.0, 3, 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Personas] WHERE [Cedula] = '203')
BEGIN
	INSERT INTO [Personas] ([Cedula],[Nombre],[Fecha],[Activo])
	VALUES ('203', 'Pepito Perez', GETDATE(), 1);
END 

IF NOT EXISTS (SELECT 1 FROM [Personas] WHERE [Cedula] = '204')
BEGIN
	INSERT INTO [Personas] ([Cedula],[Nombre],[Fecha],[Activo])
	VALUES ('204', 'Susana Suarez', GETDATE(), 1);
END 

DECLARE @persona INT;
DECLARE @metodoPago INT;

IF NOT EXISTS (SELECT 1 FROM [Facturas] WHERE [Numero] = 'H0010')
BEGIN
	SET @persona = (SELECT [id] FROM [Personas] WHERE [Cedula] = '203');
	SET @metodoPago = (SELECT [id] FROM [MetodoDePagos] WHERE [Nombre] = 'Tarjeta');

	INSERT INTO [Facturas] ([Numero], [Fecha], [Persona], [MetodoDePago], [Iva], [Total])
	VALUES ('H0010', GETDATE(), @persona, @metodoPago, 20000.0, 2040000.0);
END

IF NOT EXISTS (SELECT 1 FROM [Facturas] WHERE [Numero] = 'H0011')
BEGIN
	SET @persona = (SELECT [id] FROM [Personas] WHERE [Cedula] = '204');
	SET @metodoPago = (SELECT [id] FROM [MetodoDePagos] WHERE [Nombre] = 'Efectivo');

	INSERT INTO [Facturas] ([Numero], [Fecha], [Persona], [MetodoDePago], [Iva], [Total])
	VALUES ('H0011', GETDATE(), @persona, @metodoPago, 0.0, 2000.0);
END

IF NOT EXISTS (SELECT 1 FROM [Facturas] WHERE [Numero] = 'H0012')
BEGIN
	SET @persona = (SELECT [id] FROM [Personas] WHERE [Cedula] = '203');
	SET @metodoPago = (SELECT [id] FROM [MetodoDePagos] WHERE [Nombre] = 'Tarjeta');

	INSERT INTO [Facturas] ([Numero], [Fecha], [Persona], [MetodoDePago], [Iva], [Total])
	VALUES ('H0012', GETDATE(), @persona, @metodoPago, 15000.0, 1515000.0);
END

DECLARE @producto INT;
DECLARE @factura INT;

SET @factura = (SELECT [id] FROM [Facturas] WHERE [Numero] = 'H0010');

SET @producto = (SELECT [id] FROM [Productos] WHERE [Nombre] = 'Tv');
IF NOT EXISTS (SELECT 1 FROM [Detalles] WHERE [Factura] = @factura AND [Producto] = @producto)
BEGIN
	INSERT INTO [Detalles] ([Factura], [Producto], [Precio], [Cantidad], [Iva], [Total])
	VALUES (@factura, @producto, 2000000, 1, 20000, 2020000);
END

SET @producto = (SELECT [id] FROM [Productos] WHERE [Nombre] = 'Estencion');
IF NOT EXISTS (SELECT 1 FROM [Detalles] WHERE [Factura] = @factura AND [Producto] = @producto)
BEGIN
	INSERT INTO [Detalles] ([Factura], [Producto], [Precio], [Cantidad], [Iva], [Total])
	VALUES (@factura, @producto, 20000.0, 1, 0.0, 20000.0);
END

SET @factura = (SELECT [id] FROM [Facturas] WHERE [Numero] = 'H0011');

SET @producto = (SELECT [id] FROM [Productos] WHERE [Nombre] = 'Aguacate');
IF NOT EXISTS (SELECT 1 FROM [Detalles] WHERE [Factura] = @factura AND [Producto] = @producto)
BEGIN
	INSERT INTO [Detalles] ([Factura], [Producto], [Precio], [Cantidad], [Iva], [Total])
	VALUES (@factura, @producto, 2000, 1, 0, 2000);
END

SET @factura = (SELECT [id] FROM [Facturas] WHERE [Numero] = 'H0012');

SET @producto = (SELECT [id] FROM [Productos] WHERE [Nombre] = 'Celular');
IF NOT EXISTS (SELECT 1 FROM [Detalles] WHERE [Factura] = @factura AND [Producto] = @producto)
BEGIN
	INSERT INTO [Detalles] ([Factura], [Producto], [Precio], [Cantidad], [Iva], [Total])
	VALUES (@factura, @producto, 1500000.0, 1, 15000.0, 1515000.0);
END

CREATE PROCEDURE Proc_Obtener_Personas
AS
BEGIN
    SELECT [Id],
        [Cedula],
        [Nombre],
        [Fecha],
        [Activo] 
    FROM Personas;
END
GO