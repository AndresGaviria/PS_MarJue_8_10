using System;

namespace ConsoleApp.Clases.AlquilerPeliculas
{
    public class Personas
    {
        private int id = 0;
        private string cedula = "";
        private string nombre = "";

        public int Id { get => this.id; set => this.id = value; }
        public string Cedula { get => this.cedula; set => this.cedula = value; }
        public string Nombre { get => this.nombre; set => this.nombre = value; }
    }

    public class Tipos
    {
        private int id = 0;
        private string nombre = "";

        public int Id { get => this.id; set => this.id = value; }
        public string Nombre { get => this.nombre; set => this.nombre = value; }
    }

    public class Estados
    {
        private int id = 0;
        private string nombre = "";
        private int grupo = 0;
        private int accion = 0;

        public int Id { get => this.id; set => this.id = value; }
        public string Nombre { get => this.nombre; set => this.nombre = value; }
        public int Grupo { get => this.grupo; set => this.grupo = value; }
        public int Accion { get => this.accion; set => this.accion = value; }
    }

    public class Peliculas
    {
        private int id = 0;
        private string nombre = "";
        private string imagen = "";
        private double duracion = 0.0;
        private Tipos? tipo = null;

        public int Id { get => this.id; set => this.id = value; }
        public string Nombre { get => this.nombre; set => this.nombre = value; }
        public string Imagen { get => this.imagen; set => this.imagen = value; }
        public double Duracion { get => this.duracion; set => this.duracion = value; }
        public Tipos? Tipo { get => this.tipo; set => this.tipo = value; }
    }

    public class Prestamos
    {
        private int id = 0;
        private string numero = "";
        private Personas? persona = null;
        private DateTime? fecha = null;
        private Estados? estado = null;
        private List<Detalles> detalles = new List<Detalles>();

        public int Id { get => this.id; set => this.id = value; }
        public string Numero { get => this.numero; set => this.numero = value; }
        public Personas? Persona { get => this.persona; set => this.persona = value; }
        public DateTime? Fecha { get => this.fecha; set => this.fecha = value; }
        public Estados? Estado { get => this.estado; set => this.estado = value; }
        public List<Detalles> Detalles { get => this.detalles; set => this.detalles = value; }
    }

    public class Detalles
    {
        private int id = 0;
        private Prestamos? prestamo = null;
        private Peliculas? pelicula = null;
        private DateTime? fecha = null;
        private Estados? estado = null;

        public int Id { get => this.id; set => this.id = value; }
        public Prestamos? Prestamo { get => this.prestamo; set => this.prestamo = value; }
        public Peliculas? Pelicula { get => this.pelicula; set => this.pelicula = value; }
        public DateTime? Fecha { get => this.fecha; set => this.fecha = value; }
        public Estados? Estado { get => this.estado; set => this.estado = value; }
    }
}