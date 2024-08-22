using System;

namespace ConsoleApp.Definicion
{
    public class Clases
    {
        public void Ejecutar()
        {
            Console.WriteLine("Clases, Herencia (Abstractas, Interfaces), Polimorfismo, Encapsulamiento");
            
            // Clases
            Productos producto = new Productos(1, "TV", 2000000, 5);

            // Interfaces
            IProductosBase iProductosBase = producto;
            IProductosBase iProductosBase1 = (IProductosBase)producto;
            IProductosBase iProductosBase2 = (IProductosBase)(new Productos(1, "TV", 2000000, 5));

            // Clases abstractas
            //AProductosBase aProductosBase = new AProductosBase();
            AProductosBase aProductosBase = producto;
            AProductosBase aProductosBas2 = (AProductosBase)(new Productos(1, "TV", 2000000, 5));
            //Console.WriteLine(producto.GetNombre());
            Console.WriteLine("ID: " + producto.Id + ", " + 
                "NOMBRE: " + producto.Nombre + ", " + 
                "PRECIO: " + producto.Precio + ", " + 
                "CANTIDAD: " + producto.Cant);

            Console.WriteLine("\n");
        }
    }

    public abstract class AProductosBase
    {
        // Atributos
        protected int id = 0;
        protected string nombre = "";
        protected double precio = 0.0;
        protected double cant = 0.0;

        // Constructor
        public AProductosBase() { }
        public AProductosBase(int id, string nombre, double precio, double cant) : base()
        {
            this.id = id;
            this.nombre = nombre;
            this.precio = precio;
            this.cant = cant;
        }

        // Propiedades
        public int Id { get => this.id; set => this.id = value; }
        public string Nombre { get => this.nombre; set => this.nombre = value; }
        public double Precio { get => this.precio; set => this.precio = value; }
        public double Cant { get { return this.cant; } set { this.cant = value; } }
        // Otras opciones     
        public string GetNombre() { return this.nombre; }
        public void SetNombre(string valor) { this.nombre = valor; }
        public string Nombre2 { get => this.nombre; }

        // Metodos
        public abstract double CalcDescuento();

        protected bool ValidarCantidad()
        {
            if (this.cant < 0)
                return false;
            return true;
        }
    }

    public interface IProductosBase
    {
        double PrecioFinal();
    }

    public class Productos : AProductosBase, IProductosBase
    {
        // Constructor
        public Productos() : base() { }
        public Productos(int id, string nombre, double precio, double cant) : 
            base(id, nombre, precio, cant)
        {

        }

        // Metodos
        public double PrecioFinal() { return this.precio * this.cant; }
        
        public override double CalcDescuento() { return this.PrecioFinal(); }
    }

    public class Productos2 : AProductosBase, IProductosBase
    {
        protected double impuestos = 16;
        protected double descuento = 5;

        // Constructor
        public Productos2(int id, string nombre, double precio, double cant) : base()
        {
            this.id = id;
            this.nombre = nombre;
            this.precio = precio;
            this.cant = cant;
        }

        // Propiedades
        public double Impuestos { get => this.impuestos; set => this.impuestos = value; }
        public double Descuento { get => this.descuento; set => this.descuento = value; }

        // Metodos
        public double PrecioFinal()
        {
            return this.precio * this.cant + (((this.precio / 100) * impuestos) * this.cant);
        }
        
        public override double CalcDescuento()
        {
            var respuesta = 0.0;
            try
            {
                respuesta = this.PrecioFinal() - ((this.precio / 100) * this.descuento);
                return respuesta;
            }
            catch (OverflowException oex)
            {
                Console.WriteLine(oex.ToString());
                return respuesta;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return respuesta;
            }
        }
    }
}