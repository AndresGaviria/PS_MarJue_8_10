Productos producto = new Productos(1, "TV", 2000000, 5);
producto.CalcularDescuento();
producto.CalcularValor();

AProductosBase aProductosBase = producto;
aProductosBase.CalcularDescuento();

IProductosBase iProductosBase = producto;
iProductosBase.CalcularValor();

Console.WriteLine(producto.get_nombre());

public abstract class AProductosBase
{
    // Atributos
    protected int id = 0;
    protected string nombre = "";
    protected double precio = 0.0;
    protected double cant = 0.0;

    // Propiedades
    public int get_id() { return id; }
    public string get_nombre() { return nombre; }
    public void set_nombre(string valor) { nombre = valor; }

    public abstract bool CalcularDescuento();
}

public interface IProductosBase
{
    double CalcularValor();
}

public class Productos : AProductosBase, IProductosBase
{
    // Contructor
    public Productos(int id, string nombre, double precio, double cant)
    {
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
        this.cant = cant;
    }

    // Metodos
    public double CalcularValor()
    {
        return precio + cant;
    }

    public override bool CalcularDescuento()
    {
        return false;
    }
}

public class Productos2 : AProductosBase, IProductosBase
{
    // Atributos
    private double descuento = 0.0;

    // Contructor
    public Productos2(int id, string nombre, double precio, double cant)
    {
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
        this.cant = cant;
    }

    // Metodos
    public double CalcularValor()
    {
        return precio + cant + ((precio / 100) * 16);
    }

    public override bool CalcularDescuento()
    {
        return true;
    }
}