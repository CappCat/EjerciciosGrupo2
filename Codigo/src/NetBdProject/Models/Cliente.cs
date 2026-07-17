namespace NetBdProject.Models;

public class Cliente
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<Cuenta> Cuentas { get; set; } = new();
}