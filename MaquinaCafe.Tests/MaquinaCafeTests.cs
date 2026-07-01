using NUnit.Framework;

namespace MaquinaCafe.Tests;

[TestFixture]
public class MaquinaCafeTests
{
    private MaquinaCafe _maquina;

    [SetUp]
    public void Inicializar() => _maquina = new MaquinaCafe();

    
    [Test]
    public void InsertarMoneda_DebeAcumularSaldo()
    {
     

      
        _maquina.InsertarMoneda(25);

        Assert.That(_maquina.Saldo, Is.EqualTo(25));
    }

    [Test]
    public void SeleccionarBebida_SaldoSuficiente_RetornaTrue()
    {
     
        _maquina.InsertarMoneda(100);

       
        var resultado = _maquina.SeleccionarBebida("Cafe");

        
        Assert.That(resultado, Is.True);
    }

    [Test]
    public void SeleccionarBebida_SaldoInsuficiente_RetornaFalse()
    {
      
        _maquina.InsertarMoneda(50);

       
        var resultado = _maquina.SeleccionarBebida("Cafe");

       
        Assert.That(resultado, Is.False);
    }

   
    [Test]
    public void ObtenerCambio_DespuesDeDispensar_DevuelveCambioCorrecto()
    {
        
        _maquina.InsertarMoneda(150);
        _maquina.SeleccionarBebida("Cafe"); 

       
        var cambio = _maquina.ObtenerCambio();

        
        Assert.That(cambio, Is.EqualTo(50));
    }

    
    [Test]
    public void SeleccionarBebida_BebidaNoExiste_LanzaArgumentException()
    {
       
        Assert.Throws<ArgumentException>(() => _maquina.SeleccionarBebida("Jugo"));
    }

    
    [Test]
    public void ObtenerMenu_DebeRetornarTresBebidasConSusPrecios()
    {
        
        var menu = _maquina.ObtenerMenu();

        
        Assert.That(menu.Count, Is.EqualTo(3));
        Assert.That(menu["Cafe"].Precio, Is.EqualTo(100));
        Assert.That(menu["Te"].Precio, Is.EqualTo(75));
        Assert.That(menu["Agua"].Precio, Is.EqualTo(50));
    }

    
    [Test]
    public void DevolverMonedas_DebeReiniciarSaldoACero()
    {
        
        _maquina.InsertarMoneda(100);

        
        _maquina.DevolverMonedas();

        
        Assert.That(_maquina.Saldo, Is.EqualTo(0));
    }

    
    [Test]
    public void SeleccionarBebida_SinStock_RetornaFalse()
    {
        
        _maquina.InsertarMoneda(1000);
        for (int i = 0; i < 10; i++)
        {
            _maquina.SeleccionarBebida("Cafe");
        }

        
        var resultado = _maquina.SeleccionarBebida("Cafe");

      
        Assert.That(resultado, Is.False);
    }
}
