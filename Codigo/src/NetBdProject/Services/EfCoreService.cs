using Microsoft.EntityFrameworkCore;
using NetBdProject.Data;
using NetBdProject.DTOs;
using NetBdProject.Models;

namespace NetBdProject.Services;

public class EfCoreService
{
    public void RegistrarCuenta(int clienteId, string numeroCuenta, decimal saldoInicial)
    {
        using var context = new BancoContext();

        var clienteExiste = context.Clientes.Any(x => x.Id == clienteId);
        if (!clienteExiste)
        {
            Console.WriteLine($"No existe el cliente {clienteId}, no se pudo registrar la cuenta.");
            return;
        }

        var cuenta = new Cuenta
        {
            ClienteId = clienteId,
            NumeroCuenta = numeroCuenta,
            Saldo = saldoInicial,
            Activa = true
        };

        context.Cuentas.Add(cuenta);

        context.Movimientos.Add(new Movimiento
        {
            Cuenta = cuenta,
            Fecha = DateTime.Now,
            Monto = saldoInicial,
            Tipo = TipoMovimiento.Deposito,
            Concepto = "Apertura de cuenta"
        });

        context.SaveChanges();
        Console.WriteLine($"Cuenta {numeroCuenta} registrada con saldo inicial de {saldoInicial:C2}.");
    }

    public void ListarCuentas()
    {
        using var context = new BancoContext();

        var cuentas = context.Cuentas
            .AsNoTracking()
            .Select(c => new CuentaDTO
            {
                Id = c.Id,
                NumeroCuenta = c.NumeroCuenta,
                Titular = c.Cliente != null ? c.Cliente.NombreCompleto : string.Empty,
                Saldo = c.Saldo,
                Activa = c.Activa
            })
            .OrderBy(c => c.Id)
            .ToList();

        Console.WriteLine("Cuentas:");

        if (cuentas.Count == 0)
        {
            Console.WriteLine("  (sin datos)");
            return;
        }

        foreach (var cuenta in cuentas)
        {
            Console.WriteLine($"  #{cuenta.Id} | {cuenta.NumeroCuenta} | {cuenta.Titular} | {cuenta.SaldoFormateado} | Activa: {cuenta.Activa}");
        }
    }

    public void BuscarCuentasPorTitular(string texto)
    {
        using var context = new BancoContext();

        var cuentas = context.Cuentas
            .AsNoTracking()
            .Where(c => c.Cliente != null && c.Cliente.NombreCompleto.Contains(texto))
            .Select(c => new CuentaDTO
            {
                Id = c.Id,
                NumeroCuenta = c.NumeroCuenta,
                Titular = c.Cliente != null ? c.Cliente.NombreCompleto : string.Empty,
                Saldo = c.Saldo,
                Activa = c.Activa
            })
            .ToList();

        Console.WriteLine($"Búsqueda de cuentas por '{texto}':");

        if (cuentas.Count == 0)
        {
            Console.WriteLine("  (sin coincidencias)");
            return;
        }

        foreach (var cuenta in cuentas)
        {
            Console.WriteLine($"  {cuenta.NumeroCuenta} - {cuenta.Titular} - {cuenta.SaldoFormateado}");
        }
    }

    public void ActualizarSaldoCuenta(int id, decimal nuevoSaldo)
    {
        using var context = new BancoContext();

        var cuenta = context.Cuentas.Find(id);
        if (cuenta is null)
        {
            Console.WriteLine($"No se encontró la cuenta {id}.");
            return;
        }

        cuenta.Saldo = nuevoSaldo;
        context.Movimientos.Add(new Movimiento
        {
            CuentaId = cuenta.Id,
            Fecha = DateTime.Now,
            Monto = nuevoSaldo,
            Tipo = TipoMovimiento.Deposito,
            Concepto = "Ajuste manual de saldo"
        });

        context.SaveChanges();
        Console.WriteLine($"Saldo de la cuenta {cuenta.NumeroCuenta} actualizado a {nuevoSaldo:C2}.");
    }

    public void EliminarCuenta(int id)
    {
        using var context = new BancoContext();

        var cuenta = context.Cuentas.Find(id);
        if (cuenta is null)
        {
            Console.WriteLine($"No se encontró la cuenta {id}.");
            return;
        }

        context.Cuentas.Remove(cuenta);
        context.SaveChanges();
        Console.WriteLine($"Cuenta {id} eliminada.");
    }

    public void RealizarTransferencia(int cuentaOrigenId, int cuentaDestinoId, decimal monto, string concepto)
    {
        using var context = new BancoContext();
        using var transaccion = context.Database.BeginTransaction();

        try
        {
            var cuentaOrigen = context.Cuentas.FirstOrDefault(c => c.Id == cuentaOrigenId);
            var cuentaDestino = context.Cuentas.FirstOrDefault(c => c.Id == cuentaDestinoId);

            if (cuentaOrigen is null || cuentaDestino is null)
            {
                throw new InvalidOperationException("Alguna de las cuentas no existe.");
            }

            if (!cuentaOrigen.Activa || !cuentaDestino.Activa)
            {
                throw new InvalidOperationException("Ambas cuentas deben estar activas.");
            }

            if (cuentaOrigen.Saldo < monto)
            {
                throw new InvalidOperationException("Saldo insuficiente en la cuenta de origen.");
            }

            cuentaOrigen.Saldo -= monto;
            cuentaDestino.Saldo += monto;

            context.Movimientos.Add(new Movimiento
            {
                CuentaId = cuentaOrigen.Id,
                Fecha = DateTime.Now,
                Monto = monto,
                Tipo = TipoMovimiento.Transferencia,
                Concepto = $"Salida: {concepto}"
            });

            context.Movimientos.Add(new Movimiento
            {
                CuentaId = cuentaDestino.Id,
                Fecha = DateTime.Now,
                Monto = monto,
                Tipo = TipoMovimiento.Transferencia,
                Concepto = $"Entrada: {concepto}"
            });

            context.SaveChanges();
            transaccion.Commit();

            Console.WriteLine($"Transferencia de {monto:C2} realizada entre {cuentaOrigen.NumeroCuenta} y {cuentaDestino.NumeroCuenta}.");
        }
        catch (Exception ex)
        {
            transaccion.Rollback();
            Console.WriteLine($"No se pudo completar la transferencia: {ex.Message}");
        }
    }
}