using NetBdProject.Data;
using NetBdProject.Services;

namespace NetBdProject;

class Program
{
    static void Main()
    {
        using (var db = new BancoContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        Console.WriteLine("=== NETBDPROJECT: SISTEMA BANCARIO (ADO.NET + EF CORE) ===\n");

        var ado = new AdoNetService();
        var ef = new EfCoreService();

        Console.WriteLine("--- 1. ALTA DE CLIENTES Y CUENTAS ---");
        ado.RegistrarCliente("Ana Torres", "20-12345678-9", "ana@banco.com");
        ado.RegistrarCliente("Luis Romero", "20-98765432-1", "luis@banco.com");
        ef.RegistrarCuenta(1, "0001-00012345", 150000m);
        ef.RegistrarCuenta(2, "0001-00054321", 90000m);

        Console.WriteLine("\n--- 2. CONSULTAS INICIALES ---");
        ado.ListarClientes();
        ef.ListarCuentas();

        Console.WriteLine("\n--- 3. ACTUALIZACIONES ---");
        ado.ActualizarEmailCliente(1, "ana.torres@banco.com");
        ef.ActualizarSaldoCuenta(2, 102500m);

        Console.WriteLine("\n--- 4. BÚSQUEDA Y TRANSACCIÓN ---");
        ef.BuscarCuentasPorTitular("Ana");
        ef.RealizarTransferencia(1, 2, 25000m, "Pago de tarjeta");
        ef.ListarCuentas();

        Console.WriteLine("\n--- 5. BAJAS ---");
        ef.EliminarCuenta(2);
        ado.EliminarCliente(2);
        ado.ListarClientes();
        ef.ListarCuentas();
    }
}