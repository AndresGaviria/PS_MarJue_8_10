var producto = new Productos(1, "TV", 2000000, 5);
Console.WriteLine(producto.get_nombre());

public abstract class ProductosBase1
{
    public abstract bool Valor();
}

public interface ProductosBase2
{
    double Valor();
}

public class Productos : ProductosBase2
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
        return 16;
    }
}

public class Productos2 : ProductosBase2
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
        return 15;
    }
}
