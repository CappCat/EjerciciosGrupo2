using System;
using System.Linq;
using DesafioFinal.Data;
using DesafioFinal.Models;

namespace DesafioFinal.Services
{
    public class EfCoreService1
    {
        // CREATE
        public void RegistrarLibro(string titulo, string isbn)
        {
            using var context = new BibliotecaContext();
            context.Libros.Add(new Libro { Titulo = titulo, ISBN = isbn, Disponible = true });
            context.SaveChanges();
            Console.WriteLine($"[EF Core] Libro '{titulo}' registrado.");
        }

        // READ
        public void ConsultarLibrosDisponibles()
        {
            using var context = new BibliotecaContext();
            var libros = context.Libros.Where(l => l.Disponible).ToList();
            Console.WriteLine("\n--- Libros Disponibles ---");
            foreach (var l in libros)
            {
                Console.WriteLine($"- ID: {l.Id} | Título: {l.Titulo} | ISBN: {l.ISBN}");
            }
        }

        // UPDATE
        public void ActualizarTituloLibro(int id, string nuevoTitulo)
        {
            using var context = new BibliotecaContext();
            var libro = context.Libros.Find(id);
            if (libro != null)
            {
                libro.Titulo = nuevoTitulo;
                context.SaveChanges();
                Console.WriteLine($"[EF Core] Título actualizado a '{nuevoTitulo}'.");
            }
        }

        // DELETE
        public void EliminarLibro(int id)
        {
            using var context = new BibliotecaContext();
            var libro = context.Libros.Find(id);
            if (libro != null)
            {
                context.Libros.Remove(libro);
                context.SaveChanges();
                Console.WriteLine($"[EF Core] Libro ID {id} eliminado.");
            }
        }

        // TRANSACCIONES COMPLEJAS
        public void RealizarPrestamo(int usuarioId, int libroId)
        {
            using var context = new BibliotecaContext();
            using var trans = context.Database.BeginTransaction();
            try
            {
                var libro = context.Libros.Find(libroId);
                if (libro != null && libro.Disponible)
                {
                    libro.Disponible = false; // Cambia el estado
                    Console.WriteLine($"[EF Transacción] Préstamo exitoso. '{libro.Titulo}' ya no está disponible.");
                    context.SaveChanges();
                    trans.Commit();
                }
            }
            catch
            {
                trans.Rollback();
                Console.WriteLine("[EF Transacción] Error: Rollback ejecutado.");
            }
        }
    }
}