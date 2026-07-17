using Microsoft.EntityFrameworkCore;
using NetBdProject.Models;
using NetBdProject.Utils;

namespace NetBdProject.Data;

public class BancoContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Cuenta> Cuentas { get; set; } = null!;
    public DbSet<Movimiento> Movimientos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DatabaseHelper.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasIndex(x => x.Documento)
            .IsUnique();

        modelBuilder.Entity<Cuenta>()
            .HasIndex(x => x.NumeroCuenta)
            .IsUnique();

        modelBuilder.Entity<Cuenta>()
            .Property(x => x.Saldo)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Movimiento>()
            .Property(x => x.Monto)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Movimiento>()
            .Property(x => x.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<Cuenta>()
            .HasOne(x => x.Cliente)
            .WithMany(x => x.Cuentas)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Movimiento>()
            .HasOne(x => x.Cuenta)
            .WithMany(x => x.Movimientos)
            .HasForeignKey(x => x.CuentaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}