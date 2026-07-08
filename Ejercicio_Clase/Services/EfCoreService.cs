using System;
using System.Linq;
using EjercicioClase.Data;
using EjercicioClase.Models;

namespace EjercicioClase.Services
{
    public class EfCoreService1
    {
        public void InsertarProducto(string nombre, decimal precio, int stock)
        {
            using var context = new TiendaContext();
            context.Productos.Add(new Producto { Nombre = nombre, Precio = precio, Stock = stock });
            context.SaveChanges();
            Console.WriteLine($"[+] Producto '{nombre}' insertado.");
        }

        public void ListarProductos()
        {
            using var context = new TiendaContext();
            
            // Aquí transformamos el Modelo real en un DTO para mostrarlo
            var productosDTO = context.Productos
                .Select(p => new DTOs.ProductoDTO 
                { 
                    Nombre = p.Nombre, 
                    PrecioFormateado = $"${p.Precio}" 
                }).ToList();

            foreach (var p in productosDTO)
                Console.WriteLine($"- Producto: {p.Nombre} | Precio: {p.PrecioFormateado}");
        }

        public void ActualizarProducto(int id, decimal nuevoPrecio)
        {
            using var context = new TiendaContext();
            var p = context.Productos.Find(id);
            if (p != null) { p.Precio = nuevoPrecio; context.SaveChanges(); }
        }

        public void EliminarProducto(int id)
        {
            using var context = new TiendaContext();
            var p = context.Productos.Find(id);
            if (p != null) { context.Productos.Remove(p); context.SaveChanges(); }
        }

        public void BuscarPorNombre(string nombre)
        {
            using var context = new TiendaContext();
            var lista = context.Productos.Where(p => p.Nombre.Contains(nombre)).ToList();
            foreach(var p in lista) Console.WriteLine($"- {p.Nombre}: ${p.Precio}");
        }
    }
}