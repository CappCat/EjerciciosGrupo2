using System;
namespace DesafioFinal.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public bool Disponible { get; set; }
    }
}