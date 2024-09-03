using System;

namespace ConsoleApp.Clases.Entidades
{
    public class Modelos
    {
        public void Ejecutar()
        {
            Console.WriteLine("Clases - Modelos, Entidades");
            this.Ejemplo();
        }

        private void Ejemplo()
        {
            Console.WriteLine("Ejemplo Base");
            var lista = new List<FacturasBase>();

            var temporal = new FacturasBase() 
            {
                Id = 1,
                Numero = "H0010",
                Cedula = "203",
                Persona = "Pepito Perez",
                Fecha = new DateTime(1980, 01, 01),
                Producto = "TV",
                Precio = 2000000.0,
                Cantidad = 1.0,
                MedioDePago = "TARJETA",
                IVA = 20000,
                Total = 2040000,
            };
            lista.Add(temporal);
            lista.Add(new FacturasBase() 
            {
                Id = 2,
                Numero = "H0010",
                Cedula = "203",
                Persona = "Pepito Perez",
                Fecha = new DateTime(1980, 01, 01),
                Producto = "Estencion",
                Precio = 20000.0,
                Cantidad = 1.0,
                MedioDePago = "TARJETA",
                IVA = 0,
                Total = 2040000,
            });
            lista.Add(new FacturasBase() 
            {
                Id = 3,
                Numero = "H0011",
                Cedula = "204",
                Persona = "Susana Suarez",
                Fecha = new DateTime(1995, 01, 01),
                Producto = "Aguacate",
                Precio = 2000.0,
                Cantidad = 1.0,
                MedioDePago = "EFECTIVO",
                IVA = 0,
                Total = 2000,
            });
            lista.Add(new FacturasBase() 
            {
                Id = 4,
                Numero = "H0012",
                Cedula = "203",
                Persona = "Pepito Perez",
                Fecha = new DateTime(1980, 01, 01),
                Producto = "Celular",
                Precio = 1500000.0,
                Cantidad = 1.0,
                MedioDePago = "TARJETA",
                IVA = 15000,
                Total = 1515000,
            });

            foreach (var elemento in lista)
            {
                Console.WriteLine("|" + elemento.Id + 
                    "|" + elemento.Numero +
                    "|" + elemento.Cedula +
                    "|" + elemento.Persona + 
                    "|" + elemento.Fecha +
                    "|" + elemento.Producto +
                    "|" + elemento.Precio +
                    "|" + elemento.Cantidad + 
                    "|" + elemento.MedioDePago +
                    "|" + elemento.IVA +
                    "|" + elemento.Total);
            }
            Console.WriteLine("\n");
        }
    }

    public class FacturasBase
    {
        private int id = 0;
        private string numero = "";
        private string cedula = "";
        private string persona = "";
        private DateTime fecha = DateTime.Now;
        private string producto = "";
        private double precio = 0.0;
        private double cantidad = 0.0;
        private string medioDePago = "";
        private double iva = 0.0;
        private double total = 0.0;

        public int Id { get => this.id; set => this.id = value; }
        public string Numero { get => this.numero; set => this.numero = value; }
        public string Cedula { get => this.cedula; set => this.cedula = value; }
        public string Persona { get => this.persona; set => this.persona = value; }
        public DateTime Fecha { get => this.fecha; set => this.fecha = value; }
        public string Producto { get => this.producto; set => this.producto = value; }
        public double Precio { get => this.precio; set => this.precio = value; }
        public double Cantidad { get => this.cantidad; set => this.cantidad = value; }
        public string MedioDePago { get => this.medioDePago; set => this.medioDePago = value; }
        public double IVA { get => this.iva; set => this.iva = value; }
        public double Total { get => this.total; set => this.total = value; }
    }
}