using Microsoft.EntityFrameworkCore;
using DesafioFinal.Models;
using DesafioFinal.Utils;

namespace DesafioFinal.Data
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseHelper.ConnectionString);
        }
    }
}