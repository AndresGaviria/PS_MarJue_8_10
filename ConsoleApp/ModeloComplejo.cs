using System;

namespace ConsoleApp.Entidades
{
    public class ModeloComplejo
    {        
        public void Ejecutar()
        {
            Console.WriteLine("Clases - Modelos, Entidades, Enumerables");
            this.Ejemplo();
        }

        private void Ejemplo()
        {
            Console.WriteLine("Ejemplo Complejo");

            List<MetodoDePagos> mediosDePagos = ObtenerMetodoDePagos();
            List<Productos> productos = ObtenerProductos();
            List<Personas> personas = ObtenerPersonas();
            var facturas = ObtenerFacturas(mediosDePagos, productos, personas);

            ImprimirFacturas(facturas);

            // Consultas
            var consulta = facturas.Where(x => x.Persona?.Cedula == "203").ToList();
            ImprimirFacturas(consulta);

            var factura = facturas.FirstOrDefault(x => x.Persona?.Cedula == "204");
            ImprimirFacturas([ factura! ]);
            
            Console.WriteLine("\n");
        }

        private Facturas CrearFactura(int id, string numero, DateTime fecha, 
            Personas persona, MetodoDePagos metodoDePago, List<Detalles> detalles)
        {
            var factura = new Facturas() 
            {
                Id = id,
                Numero = numero,
                Fecha = fecha,
                Persona = persona,
                MetodoDePago = metodoDePago,
                Detalles = detalles,
            };

            var total = 0.0;
            var iva = 0.0;
            foreach (var detaille in factura.Detalles.ToList())
            {
                detaille.Factura = factura;
                iva += detaille.IVA;
                total = total + detaille.Total;
            }
            factura.IVA = iva;
            factura.Total = total;
            return factura;
        }

        private Detalles CrearDetalle(int id, Productos producto, int cantidad)
        {
            var detalle = new Detalles();
            detalle.Id = id;
            detalle.Producto = producto;
            detalle.Precio = producto.Precio;
            detalle.Cantidad = cantidad;
            detalle.IVA = ((producto.Precio / 100) * producto.IVA) * cantidad;
            // Despues de impuestos
            // detalle.Total = (producto.Precio * cantidad);
            // Antes de impuestos
            detalle.Total = (producto.Precio * cantidad) + detalle.IVA;
            return detalle;
        }

        private void ImprimirFacturas(IEnumerable<Facturas> facturas)
        {
            foreach (var factura in facturas)
            {
                Console.WriteLine("|" + factura.Id + 
                    "|" + factura.Numero +
                    "|" + factura.Persona?.Cedula +
                    "|" + factura.Persona?.Nombre + 
                    "|" + factura.Fecha +
                    "|" + factura.MetodoDePago?.Nombre + 
                    "|" + factura.IVA +
                    "|" + factura.Total);

                    foreach (var detalle in factura.Detalles)
                    {
                        Console.WriteLine(" * " + detalle.Id + 
                            // "|" + detalle.Factura?.Numero +
                            "|" + detalle.Producto?.Nombre +
                            "|" + detalle.Precio +
                            "|" + detalle.Cantidad + 
                            "|" + detalle.IVA +
                            "|" + detalle.Total);
                    }
            }
            Console.WriteLine("\n");
        }

        private List<MetodoDePagos> ObtenerMetodoDePagos()
        {
            // var tipo = (MetodoDePagos.TipodePagos)1;

            List<MetodoDePagos> lista = new List<MetodoDePagos>();
            lista.Add(new MetodoDePagos() 
            {
                Id = 1,
                Nombre = "Efectivo",
                Tipo = (int)MetodoDePagos.TipodePagos.EFECTIVO,
            });
            lista.Add(new MetodoDePagos() 
            {
                Id = 2,
                Nombre = "Tarjeta Visa",
                Tipo = (int)MetodoDePagos.TipodePagos.TARJETA,
            });
            return lista;
        }

        private List<Productos> ObtenerProductos()
        {
            List<Productos> lista = new List<Productos>();
            lista.Add(new Productos() 
            {
                Id = 1,
                Nombre = "Tv",
                Precio = 2000000,
                Cantidad = 5,
                IVA = 1,
            });
            lista.Add(new Productos() 
            {
                Id = 2,
                Nombre = "Aguacate",
                Precio = 2000,
                Cantidad = 20,
                IVA = 0,
            });
            lista.Add(new Productos() 
            {
                Id = 3,
                Nombre = "Celular",
                Precio = 1500000,
                Cantidad = 10,
                IVA = 1,
            });
            lista.Add(new Productos() 
            {
                Id = 4,
                Nombre = "Estencion",
                Precio = 20000,
                Cantidad = 3,
                IVA = 0,
            });
            return lista;
        }

        private List<Personas> ObtenerPersonas()
        {
            List<Personas> lista = new List<Personas>();
            lista.Add(new Personas() 
            {
                Id = 1,
                Cedula = "203",
                Nombre = "Pepito Perez",
                Fecha = new DateTime(1980, 01, 01),
                Activo = true,
            });
            lista.Add(new Personas() 
            {
                Id = 2,
                Cedula = "204",
                Nombre = "Susana Suarez",
                Fecha = new DateTime(1995, 01, 01),
                Activo = true,
            });
            return lista;
        }

        private IEnumerable<Facturas> ObtenerFacturas(List<MetodoDePagos> mediosDePagos, 
            List<Productos> productos, List<Personas> personas)
        {
            var facturas = new List<Facturas>();
            facturas.Add(
                CrearFactura(
                    1, 
                    "H0010", 
                    DateTime.Now, 
                    personas[0], 
                    mediosDePagos[1],
                    [
                        CrearDetalle(
                            1, 
                            productos[0], 
                            1), 
                        CrearDetalle(
                            2, 
                            productos[3], 
                            1)
                    ])
            );
            facturas.Add(
                CrearFactura(
                    2, 
                    "H0011", 
                    DateTime.Now, 
                    personas[1], 
                    mediosDePagos[0],
                    [
                        CrearDetalle(
                            3, 
                            productos[1], 
                            1), 
                    ])
            );
            facturas.Add(
                CrearFactura(
                    3, 
                    "H0012", 
                    DateTime.Now, 
                    personas[0], 
                    mediosDePagos[1],
                    [
                        CrearDetalle(
                            4, 
                            productos[2], 
                            1), 
                    ])
            );
            return facturas;
        }
    }

    public class Productos
    {
        private int id = 0;
        private string nombre = "";
        private double precio = 0.0;
        private double cantidad = 0.0;
        private double iva = 0.0;

        public int Id { get => this.id; set => this.id = value; }
        public string Nombre { get => this.nombre; set => this.nombre = value; }
        public double Precio { get => this.precio; set => this.precio = value; }
        public double Cantidad { get => this.cantidad; set => this.cantidad = value; }
        public double IVA { get => this.iva; set => this.iva = value; }
    }

    public class MetodoDePagos
    {
        public enum TipodePagos { EFECTIVO = 0, TARJETA = 1 }

        private int id = 0;
        private string nombre = "";
        private int tipo = (int)TipodePagos.EFECTIVO;

        public int Id { get => this.id; set => this.id = value; }
        public string Nombre { get => this.nombre; set => this.nombre = value; }
        public int Tipo { get => this.tipo; set => this.tipo = value; }
    }
    
    public class Personas
    {
        private int id = 0;
        private string cedula = "";
        private string nombre = "";
        private DateTime fecha = DateTime.Now;
        private bool activo = false;

        public int Id { get => this.id; set => this.id = value; }
        public string Cedula { get => this.cedula; set => this.cedula = value; }
        public string Nombre { get => this.nombre; set => this.nombre = value; }
        public DateTime Fecha { get => this.fecha; set => this.fecha = value; }
        public bool Activo { get => this.activo; set => this.activo = value; }
    }
    
    public class Facturas
    {
        private int id = 0;
        private string numero = "";
        private DateTime fecha = DateTime.Now;
        private Personas? persona = null;
        private MetodoDePagos? metodoDePago = null;
        private double iva = 0.0;
        private double total = 0.0;
        private List<Detalles> detalles = new List<Detalles>();

        public int Id { get => this.id; set => this.id = value; }
        public string Numero { get => this.numero; set => this.numero = value; }
        public DateTime Fecha { get => this.fecha; set => this.fecha = value; }
        public Personas? Persona { get => this.persona; set => this.persona = value; }
        public MetodoDePagos? MetodoDePago { get => this.metodoDePago; set => this.metodoDePago = value; }
        public double IVA { get => this.iva; set => this.iva = value; }
        public double Total { get => this.total; set => this.total = value; }
        public List<Detalles> Detalles { get => this.detalles; set => this.detalles = value; }
    }
    
    public class Detalles
    {
        private int id = 0;
        private Facturas? factura = null;
        private Productos? producto = null;
        private double precio = 0.0;
        private double cantidad = 0.0;
        private double iva = 0.0;
        private double total = 0.0;

        public int Id { get => this.id; set => this.id = value; }
        public Facturas? Factura { get => this.factura; set => this.factura = value; }
        public Productos? Producto { get => this.producto; set => this.producto = value; }
        public double Precio { get => this.precio; set => this.precio = value; }
        public double Cantidad { get => this.cantidad; set => this.cantidad = value; }
        public double IVA { get => this.iva; set => this.iva = value; }
        public double Total { get => this.total; set => this.total = value; }
    }
}