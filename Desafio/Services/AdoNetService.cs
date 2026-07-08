using System;
using Microsoft.Data.SqlClient;
using DesafioFinal.Utils;

namespace DesafioFinal.Services
{
    public class AdoNetService1
    {
        // CREATE
        public void RegistrarUsuario(string nombre, string email)
        {
            using var conn = new SqlConnection(DatabaseHelper.ConnectionString);
            string query = "INSERT INTO Usuarios (Nombre, Email) VALUES (@Nombre, @Email)";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Email", email);
            
            conn.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine($"[ADO.NET] Usuario '{nombre}' registrado.");
        }

        // READ
        public void ListarUsuarios()
        {
            using var conn = new SqlConnection(DatabaseHelper.ConnectionString);
            string query = "SELECT Id, Nombre, Email FROM Usuarios";
            using var cmd = new SqlCommand(query, conn);
            
            conn.Open();
            using var reader = cmd.ExecuteReader();
            Console.WriteLine("\n--- Lista de Usuarios ---");
            while (reader.Read())
            {
                Console.WriteLine($"- ID: {reader["Id"]} | Nombre: {reader["Nombre"]} | Email: {reader["Email"]}");
            }
        }

        // UPDATE
        public void ActualizarEmailUsuario(int id, string nuevoEmail)
        {
            using var conn = new SqlConnection(DatabaseHelper.ConnectionString);
            string query = "UPDATE Usuarios SET Email = @Email WHERE Id = @Id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", nuevoEmail);
            cmd.Parameters.AddWithValue("@Id", id);
            
            conn.Open();
            int filas = cmd.ExecuteNonQuery();
            if(filas > 0) Console.WriteLine($"[ADO.NET] Email actualizado para el usuario ID {id}.");
        }

        // DELETE
        public void EliminarUsuario(int id)
        {
            using var conn = new SqlConnection(DatabaseHelper.ConnectionString);
            string query = "DELETE FROM Usuarios WHERE Id = @Id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            
            conn.Open();
            int filas = cmd.ExecuteNonQuery();
            if(filas > 0) Console.WriteLine($"[ADO.NET] Usuario ID {id} eliminado.");
        }
    }
}