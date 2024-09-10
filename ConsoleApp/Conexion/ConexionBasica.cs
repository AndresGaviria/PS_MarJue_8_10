using System.Data.SqlClient;
using System.Data;

namespace ConsoleApp.Conexion
{
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