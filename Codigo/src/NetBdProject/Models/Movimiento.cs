namespace NetBdProject.Models;

public class Movimiento
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Monto { get; set; }
    public TipoMovimiento Tipo { get; set; }
    public string Concepto { get; set; } = string.Empty;

    public int CuentaId { get; set; }
    public Cuenta? Cuenta { get; set; }
}