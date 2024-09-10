using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp.Conexion
{
    public class ConexionEF
    {
        private string string_conexion = "server=localhost;database=db_facturas;Integrated Security=True;TrustServerCertificate=true;";
        // server=localhost;database=db_facturas;uid=sa;pwd=Clas3sPrO2024_!;TrustServerCertificate=true;
        // server=localhost;database=db_facturas;Integrated Security=True;TrustServerCertificate=true;

        public ConexionEF()
        {
            Console.WriteLine("\n\n\n\n CONEXION EF A BASE DE DATOS");
        }

        public void ObtenerPeliculas()
        {
            var conexion = new Conexion();
            conexion.StringConnection = this.string_conexion;

            var lista_peliculas = conexion.Peliculas.ToList();

            foreach (var pelicula in lista_peliculas)
            {
                Console.WriteLine(pelicula.Id.ToString() + "|" + pelicula.Nombre);
            }
        }

        public void GuardarPeliculas()
        {
            var conexion = new Conexion();
            conexion.StringConnection = this.string_conexion;

            var pelicula = new Peliculas()
            {
                Id = 0,
                Nombre = "Prueba EF",
            };
            conexion.Peliculas.Add(pelicula);
            conexion.SaveChanges();
        }

        public void ObtenerCarteleras()
        {
            var conexion = new Conexion();
            conexion.StringConnection = this.string_conexion;

            var lista_carteleras = conexion.Carteleras
                .Include(x => x._Pelicula)
                .ToList();            
            
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

    public partial class Conexion : DbContext
    {
        public string? StringConnection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConnection!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Peliculas>? Peliculas { get; set; }
        public DbSet<Carteleras>? Carteleras { get; set; }
    }
}