namespace MaquinaCafe;

public class Program
{
    public static void Main(string[] args)
    {
        var maquina = new MaquinaCafe();

        Console.WriteLine("=== Máquina de Café (ITLA - Práctica TDD) ===");
        Console.WriteLine();
        Console.WriteLine("Menú disponible:");
        foreach (var bebida in maquina.ObtenerMenu().Values)
        {
            Console.WriteLine($"  - {bebida.Nombre}: {bebida.Precio} (stock: {bebida.Stock})");
        }

        Console.WriteLine();
        Console.WriteLine("Insertando 100...");
        maquina.InsertarMoneda(100);
        Console.WriteLine($"Saldo actual: {maquina.Saldo}");

        Console.WriteLine("Seleccionando 'Cafe'...");
        bool exito = maquina.SeleccionarBebida("Cafe");
        Console.WriteLine($"¿Se pudo dispensar? {exito}");
        Console.WriteLine($"Saldo restante: {maquina.Saldo}");

        int cambio = maquina.ObtenerCambio();
        Console.WriteLine($"Cambio entregado: {cambio}");
    }
}
