using Microsoft.Data.SqlClient;
using NetBdProject.Utils;

namespace NetBdProject.Services;

public class AdoNetService
{
    public void RegistrarCliente(string nombreCompleto, string documento, string email)
    {
        const string sql = @"
            INSERT INTO Clientes (NombreCompleto, Documento, Email)
            VALUES (@NombreCompleto, @Documento, @Email)";

        using var connection = new SqlConnection(DatabaseHelper.ConnectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
        command.Parameters.AddWithValue("@Documento", documento);
        command.Parameters.AddWithValue("@Email", email);

        connection.Open();
        var filas = command.ExecuteNonQuery();
        Console.WriteLine($"Cliente registrado con éxito ({filas} fila(s) afectada(s)).");
    }

    public void ListarClientes()
    {
        const string sql = "SELECT Id, NombreCompleto, Documento, Email FROM Clientes ORDER BY Id";

        using var connection = new SqlConnection(DatabaseHelper.ConnectionString);
        using var command = new SqlCommand(sql, connection);

        connection.Open();
        using var reader = command.ExecuteReader();

        Console.WriteLine("Clientes registrados:");

        if (!reader.HasRows)
        {
            Console.WriteLine("  (sin datos)");
            return;
        }

        while (reader.Read())
        {
            Console.WriteLine($"  #{reader.GetInt32(0)} | {reader.GetString(1)} | {reader.GetString(2)} | {reader.GetString(3)}");
        }
    }

    public void ActualizarEmailCliente(int id, string nuevoEmail)
    {
        const string sql = @"
            UPDATE Clientes
            SET Email = @Email
            WHERE Id = @Id";

        using var connection = new SqlConnection(DatabaseHelper.ConnectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Email", nuevoEmail);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        var filas = command.ExecuteNonQuery();
        Console.WriteLine(filas > 0
            ? $"Email del cliente {id} actualizado."
            : $"No se encontró el cliente {id}.");
    }

    public void EliminarCliente(int id)
    {
        const string sql = "DELETE FROM Clientes WHERE Id = @Id";

        using var connection = new SqlConnection(DatabaseHelper.ConnectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        var filas = command.ExecuteNonQuery();
        Console.WriteLine(filas > 0
            ? $"Cliente {id} eliminado."
            : $"No se encontró el cliente {id}.");
    }
}