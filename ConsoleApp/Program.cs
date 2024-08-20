Productos producto = new Productos(1, "TV", 2000000, 5);
Console.WriteLine(producto.get_nombre());

public abstract class AProductosBase
{
    public abstract bool Valor();
}

public interface IProductosBase
{
    double Valor();
}

public class Productos : IProductosBase
{
    // Atributos
    private int id = 0;
    private string nombre = "";
    private double precio = 0.0;
    private double cant = 0.0;

    // Contructor
    public Productos(int id, string nombre, double precio, double cant)
    {
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
        this.cant = cant;
    }

    // Propiedades
    public int get_id() { return id; }
    public string get_nombre() { return nombre; }
    public void set_nombre(string valor) { nombre = valor; }

    // Metodos
    public double Valor()
    {
        return precio + cant;
    }
}

public class Productos2 : IProductosBase
{
    // Atributos
    private int id = 0;
    private string nombre = "";
    private double precio = 0.0;
    private double cant = 0.0;

    // Contructor
    public Productos2(int id, string nombre, double precio, double cant)
    {
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
        this.cant = cant;
    }

    // Metodos
    public double Valor()
    {
        return precio + cant + ((precio / 100) * 16);
    }
}