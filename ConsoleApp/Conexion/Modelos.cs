using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp.Conexion
{
    public class Peliculas
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        
        [NotMapped] public virtual ICollection<Carteleras>? Carteleras { get; set; }
    }

    public class Carteleras
    {
        [Key] public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int Pelicula { get; set; }
        public bool Activo { get; set; }

        [ForeignKey("Pelicula")] public virtual Peliculas? _Pelicula { get; set; }
    }
}