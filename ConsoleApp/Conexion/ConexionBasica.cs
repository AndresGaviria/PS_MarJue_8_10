using System.Data.SqlClient;
using System.Data;

namespace ConsoleApp.Conexion
{
    public class Tests
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ConexionBasica
    {
        private string string_conexion = "server=localhost;database=db_facturas;uid=sa;pwd=Clas3sPrO2024_!;TrustServerCertificate=true;";

        public void ObtenerTests()
        {
            var sql_conexion = new SqlConnection(this.string_conexion);
            sql_conexion.Open();

            var consulta = "SELECT [Id],[Nombre] FROM Tests";
            var adaptador = new SqlDataAdapter(new SqlCommand(consulta, sql_conexion));
            var set_de_datos = new DataSet();
            adaptador.Fill(set_de_datos);

            var lista_tests = new List<Tests>();
            foreach (var elemento in set_de_datos.Tables[0].AsEnumerable())
            {
                lista_tests.Add(new Tests()
                {
                    Id = Convert.ToInt32(elemento.ItemArray[0]!.ToString()),
                    Nombre = elemento.ItemArray[1]!.ToString(),
                });
            }
            
            sql_conexion.Close();

            foreach (var test in lista_tests)
            {
                Console.WriteLine(test.Id.ToString() + "|" + test.Nombre);
            }
        }

    }
}

/*
CREATE DATABASE db_facturas
GO
USE db_facturas;
GO

CREATE TABLE [Tests]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED ([Id])
)
GO

SELECT * FROM Tests

INSERT INTO [Tests] ([Nombre]) VALUES ('Pepito');
INSERT INTO [Tests] ([Nombre]) VALUES ('Susana');
INSERT INTO [Tests] ([Nombre]) VALUES ('Felipe');
*/