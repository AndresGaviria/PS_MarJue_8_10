using System.Data.SqlClient;
using System.Data;

namespace ConsoleApp.Conexion
{
    public class Peliculas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Carteleras
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int Pelicula { get; set; }
        public bool Activo { get; set; }
        public Peliculas _Pelicula { get; set; }
    }

    public class ConexionBasica
    {
        private string string_conexion = "server=localhost;database=db_facturas;uid=sa;pwd=Clas3sPrO2024_!;TrustServerCertificate=true;";

        public ConexionBasica()
        {
            Console.WriteLine("\n\n\n\n CONEXION A BASE DE DATOS");
        }

        public void ObtenerPeliculas()
        {
            var sql_conexion = new SqlConnection(this.string_conexion);
            sql_conexion.Open();

            var consulta = "SELECT [Id],[Nombre] FROM Peliculas";
            var adaptador = new SqlDataAdapter(new SqlCommand(consulta, sql_conexion));
            var set_de_datos = new DataSet();
            adaptador.Fill(set_de_datos);

            var lista_peliculas = new List<Peliculas>();
            foreach (var elemento in set_de_datos.Tables[0].AsEnumerable())
            {
                lista_peliculas.Add(new Peliculas()
                {
                    Id = Convert.ToInt32(elemento.ItemArray[0].ToString()),
                    Nombre = elemento.ItemArray[1].ToString(),
                });
            }
            sql_conexion.Close();
            
            foreach (var pelicula in lista_peliculas)
            {
                Console.WriteLine(pelicula.Id.ToString() + "|" + pelicula.Nombre);
            }
        }

        public void NonQueryPeliculas()
        {
            var sql_conexion = new SqlConnection(this.string_conexion);
            sql_conexion.Open();

            var pelicula = new Peliculas() { Id = 0, Nombre = "Test" };

            var comando = new SqlCommand("INSERT INTO Peliculas " +
                "([Nombre])  VALUES ('" + pelicula.Nombre + "');", sql_conexion);
            var resultado = comando.ExecuteNonQuery();
            Console.WriteLine("Filas afectadas: " + resultado);
            sql_conexion.Close();
        }

        public void ObtenerPeliculas_Procedimiento()
        {
            var sql_conexion = new SqlConnection(this.string_conexion);
            sql_conexion.Open();

            var consulta = "Proc_Obtener_Peliculas";
            var comando = new SqlCommand(consulta, sql_conexion);
            comando.CommandType = CommandType.StoredProcedure;

            var adaptador = new SqlDataAdapter(comando);
            var set_de_datos = new DataSet();
            adaptador.Fill(set_de_datos);

            var lista_peliculas = new List<Peliculas>();
            foreach (var elemento in set_de_datos.Tables[0].AsEnumerable())
            {
                lista_peliculas.Add(new Peliculas()
                {
                    Id = Convert.ToInt32(elemento.ItemArray[0].ToString()),
                    Nombre = elemento.ItemArray[1].ToString(),
                });
            }
            sql_conexion.Close();
            
            foreach (var pelicula in lista_peliculas)
            {
                Console.WriteLine(pelicula.Id.ToString() + "|" + pelicula.Nombre);
            }
        }
        public void ObtenerCartelera()
        {
            var sql_conexion = new SqlConnection(this.string_conexion);
            sql_conexion.Open();

            var consulta = 
                " SELECT C.[Id] " +
                "    ,C.[Titulo] " +
                "    ,C.[Descripcion] " +
                "    ,C.[Fecha] " +
                "    ,C.[Pelicula] " +
                "    ,C.[Activo] " +
                "    ,P.[Id] " +
                "    ,P.[Nombre] " +
                " FROM [Carteleras] C " +
                "    INNER JOIN [Peliculas] P ON C.[Pelicula] = P.[Id]";
            var adaptador = new SqlDataAdapter(new SqlCommand(consulta, sql_conexion));
            var set_de_datos = new DataSet();
            adaptador.Fill(set_de_datos);

            var lista_carteleras = new List<Carteleras>();
            foreach (var elemento in set_de_datos.Tables[0].AsEnumerable())
            {
                lista_carteleras.Add(new Carteleras()
                {
                    Id = Convert.ToInt32(elemento.ItemArray[0].ToString()),
                    Titulo = elemento.ItemArray[1].ToString(),
                    Descripcion = elemento.ItemArray[2].ToString(),
                    Fecha = Convert.ToDateTime(elemento.ItemArray[3].ToString()),
                    Pelicula = Convert.ToInt32(elemento.ItemArray[4].ToString()),
                    Activo = Convert.ToBoolean(elemento.ItemArray[5].ToString()),
                    _Pelicula = new Peliculas() 
                    {
                        Id = Convert.ToInt32(elemento.ItemArray[6].ToString()),
                        Nombre = elemento.ItemArray[7].ToString(),
                    }
                });
            }
            sql_conexion.Close();
            
            foreach (var cartelera in lista_carteleras)
            {
                Console.WriteLine(
                    cartelera.Id.ToString() + " | " + 
                    cartelera.Titulo + " | " + 
                    cartelera.Descripcion + " | " + 
                    cartelera.Fecha.ToString() + " | " + 
                    cartelera.Pelicula + " | " + 
                    cartelera._Pelicula.Nombre + " | " + 
                    cartelera.Activo);
            }
        }
    }
}

/*

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

*/