namespace MaquinaCafe;

public record Bebida(string Nombre, int Precio, int Stock);

public class MaquinaCafe
{
    private int _saldo;
    private readonly Dictionary<string, Bebida> _menu;

    public int Saldo => _saldo;

    public MaquinaCafe()
    {
        _menu = new()
        {
            ["Cafe"] = new Bebida("Cafe", 100, Stock: 10),
            ["Te"] = new Bebida("Te", 75, Stock: 10),
            ["Agua"] = new Bebida("Agua", 50, Stock: 10),
        };
    }

    public void InsertarMoneda(int monto) => _saldo += monto;

    
    public bool SeleccionarBebida(string nombre)
    {
        var bebida = ObtenerBebidaOLanzar(nombre);

        if (_saldo < bebida.Precio || bebida.Stock == 0)
            return false;

        _saldo -= bebida.Precio;
        _menu[nombre] = bebida with { Stock = bebida.Stock - 1 };
        return true;
    }

    private Bebida ObtenerBebidaOLanzar(string nombre)
        => _menu.TryGetValue(nombre, out var bebida)
            ? bebida
            : throw new ArgumentException($"No existe la bebida '{nombre}'");

    public int ObtenerCambio()
    {
        var cambio = _saldo;
        _saldo = 0;
        return cambio;
    }

    
    public void DevolverMonedas() => _saldo = 0;

    
    public Dictionary<string, Bebida> ObtenerMenu() => _menu;
}
