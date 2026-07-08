using System;
using DesafioFinal.Data;
using DesafioFinal.Services;

namespace DesafioFinal
{
    class Program
    {
        static void Main()
        {
            using (var db = new BibliotecaContext()) 
            {
                db.Database.EnsureDeleted(); // Limpia la BD para que no se acumulen datos
                db.Database.EnsureCreated(); 
            }

            Console.WriteLine("=== DESAFÍO: SISTEMA DE BIBLIOTECA (ADO.NET + EF CORE) ===\n");
            
            var ef = new EfCoreService();
            var ado = new AdoNetService();

            // 1. CREATE
            Console.WriteLine("--- 1. INSERCIÓN DE DATOS ---");
            ado.RegistrarUsuario("Alex Palma", "alex@correo.com");
            ado.RegistrarUsuario("María Lopez", "maria@correo.com");
            ef.RegistrarLibro("C# Avanzado", "111-222");
            ef.RegistrarLibro("Patrones de Diseño", "333-444");

            // 2. READ
            ado.ListarUsuarios();
            ef.ConsultarLibrosDisponibles();

            // 3. UPDATE
            Console.WriteLine("\n--- 2. ACTUALIZACIONES ---");
            ado.ActualizarEmailUsuario(1, "alex.palma@nuevo.com");
            ef.ActualizarTituloLibro(2, "Patrones de Diseño: Edición 2026");

            // 4. TRANSACCIÓN
            Console.WriteLine("\n--- 3. TRANSACCIONES (PRÉSTAMOS) ---");
            ef.RealizarPrestamo(1, 1); // Alex pide prestado "C# Avanzado"
            ef.ConsultarLibrosDisponibles(); // Ya no debería salir "C# Avanzado"

            // 5. DELETE
            Console.WriteLine("\n--- 4. ELIMINACIÓN ---");
            ado.EliminarUsuario(2); // Eliminamos a María
            ado.ListarUsuarios(); // Mostramos que ya no está
        }
    }
}