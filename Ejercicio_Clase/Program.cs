using System;
using EjercicioClase.Data;
using EjercicioClase.Services;

namespace EjercicioClase
{
    class Program
    {
        static void Main()
        {
            using (var db = new TiendaContext()) 
            {
                db.Database.EnsureDeleted(); // Borra la base de datos si existe
                db.Database.EnsureCreated(); // La crea de nuevo, limpia
            }
            
            Console.WriteLine("=== EJERCICIO PRÁCTICO: CRUD CON EF CORE ===\n");
            var service = new EfCoreService();
            
            // 2. Insertar
            service.InsertarProducto("Laptop", 899.99m, 10);
            service.InsertarProducto("Mouse", 29.99m, 25);
            service.InsertarProducto("Teclado", 79.99m, 15);

            // 3. Listar
            Console.WriteLine("\nLista de productos:");
            service.ListarProductos();

            // 4. Buscar
            Console.WriteLine("\nBúsqueda por 'Lap':");
            service.BuscarPorNombre("Lap");

            // 5. Actualizar
            Console.WriteLine("\nActualizando precio de Laptop...");
            service.ActualizarProducto(1, 799.99m);

            // 6. Listar post-actualización
            Console.WriteLine("\nProductos después de actualización:");
            service.ListarProductos();

            // 7. Eliminar
            Console.WriteLine("\nEliminando Mouse...");
            service.EliminarProducto(2);

            // 8. Listar final
            Console.WriteLine("\nProductos después de eliminación:");
            service.ListarProductos();
        }
    }
}