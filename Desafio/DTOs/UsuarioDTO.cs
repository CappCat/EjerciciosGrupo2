namespace DesafioFinal.DTOs
{
    public class UsuarioDTO
    {
        // A veces no queremos enviar el ID ni contraseñas, solo info básica
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}