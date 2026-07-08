using Microsoft.EntityFrameworkCore;
using EjercicioClase.Models;
using EjercicioClase.Utils;

namespace EjercicioClase.Data
{
    public class TiendaContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseHelper.ConnectionString);
        }
    }
}