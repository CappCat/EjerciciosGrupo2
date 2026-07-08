## 🚀 Ejercicio 3.3: CRUD Completo con Entity Framework Core

### 📖 Contexto
Entity Framework Core es el ORM (Object-Relational Mapper) oficial de Microsoft. Nos permite trabajar con bases de datos utilizando objetos de C# en lugar de escribir consultas SQL manuales. En este ejercicio, gestionaremos el inventario de una tienda. Para separar la información de la base de datos de lo que mostramos al usuario, utilizaremos un patrón de diseño llamado **DTO (Data Transfer Object)**.

### 🎯 Objetivo
Completar el ciclo de vida de los datos (Create, Read, Update, Delete) para la entidad `Producto` utilizando exclusivamente el contexto de base de datos (`TiendaContext`) y consultas LINQ.

### 📝 Instrucciones
1. Abre la carpeta `Ejercicio_Clase` en tu editor.
2. Dirígete al archivo `Services/EfCoreService.cs`.
3. Encontrarás 5 métodos vacíos (`InsertarProducto`, `ListarProductos`, `ActualizarProducto`, `EliminarProducto`, `BuscarPorNombre`).
4. Reemplaza los comentarios marcados con `TODO` con el código C# correspondiente utilizando `TiendaContext`.
5. Ejecuta el programa con `dotnet run`.

**Resultado esperado:** La consola deberá mostrar un flujo sin errores donde se insertan productos, se listan con el precio formateado (gracias al DTO), se realiza una búsqueda parcial, se actualiza un precio y finalmente se elimina un registro, mostrando la lista actualizada en cada paso.