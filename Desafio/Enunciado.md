## 🏆 Desafío Final 3.4: Sistema de Biblioteca Híbrido (ADO.NET + EF Core)

### 📖 Contexto
En proyectos empresariales a gran escala, es común combinar tecnologías. EF Core es excelente para la lógica de negocio compleja y la velocidad de desarrollo, mientras que ADO.NET es inmejorable cuando necesitamos ejecutar consultas rápidas y directas con el máximo rendimiento. 

En este desafío, construiremos el motor de una biblioteca. Utilizaremos **ADO.NET** para gestionar a los Usuarios (CRUD simple y rápido) y **EF Core** para gestionar los Libros y la complejidad de los Préstamos, asegurando la integridad de los datos mediante **Transacciones**.

### 🎯 Objetivo
Desarrollar un sistema de acceso a datos híbrido, implementando consultas SQL parametrizadas para la gestión de usuarios y métodos ORM seguros para operaciones críticas de inventario.

### 📝 Instrucciones
1. Abre la carpeta `Desafio` en tu editor.
2. Dirígete primero al archivo `Services/AdoNetService.cs`.
   * Completa los 4 métodos del CRUD de Usuarios (`RegistrarUsuario`, `ListarUsuarios`, `ActualizarEmailUsuario`, `EliminarUsuario`).
   * **Regla estricta:** Debes usar `SqlConnection`, `SqlCommand` y consultas SQL puras (`INSERT`, `SELECT`, `UPDATE`, `DELETE`).
3. Dirígete al archivo `Services/EfCoreService.cs`.
   * Completa el CRUD para la entidad Libro utilizando `BibliotecaContext`.
   * Presta especial atención al método `RealizarPrestamo`. Deberás implementar una **Transacción de base de datos** (`BeginTransaction`, `Commit`, `Rollback`) para asegurar que el cambio de disponibilidad del libro solo se guarde si el proceso completo tiene éxito.
4. Ejecuta el programa con `dotnet run`.

**Resultado esperado:** La consola mostrará la ejecución de ambos servicios trabajando en conjunto sobre la misma base de datos. Se registrarán usuarios (ADO.NET) y libros (EF Core), se actualizarán datos, se ejecutará el préstamo de un libro afectando su disponibilidad, y finalmente se eliminarán registros, demostrando el dominio total de ambas herramientas.