namespace DesafioFinal.DTOs
{
    public class LibroDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty; // Dirá "Disponible" o "Prestado"
    }
}