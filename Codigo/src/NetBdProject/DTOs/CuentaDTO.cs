namespace NetBdProject.DTOs;

public class CuentaDTO
{
    public int Id { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public string Titular { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public bool Activa { get; set; }

    public string SaldoFormateado => Saldo.ToString("C2");
}