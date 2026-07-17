namespace NetBdProject.Models;

public class Cuenta
{
    public int Id { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public bool Activa { get; set; }

    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public List<Movimiento> Movimientos { get; set; } = new();
}