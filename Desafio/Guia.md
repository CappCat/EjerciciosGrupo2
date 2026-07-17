# Guía Práctica: Acceso a Datos en .NET (Grupo 2)

¡Bienvenidos a la práctica de acceso a datos! En esta sesión, exploraremos cómo interactuar con bases de datos en aplicaciones .NET utilizando dos enfoques fundamentales: el moderno **Entity Framework Core (EF Core)** y el tradicional **ADO.NET**.

---

## 🛠️ Requisitos Previos e Instalación

Antes de comenzar a escribir código, asegúrate de tener el siguiente entorno configurado en tu equipo:

1. **.NET SDK:** Instalar la versión más reciente (recomendado .NET 8) desde la página oficial de Microsoft.
2. **Editor de Código:** Visual Studio Code (con la extensión "C# Dev Kit") o Visual Studio 2022.
3. **Motor de Base de Datos:** PostgreSQL (gratuito). Debe estar en ejecución como una instancia local.
4. **Cliente SQL:** Recomendado para gestionar las bases de datos(pgAdmin).

---

## 🔁 Giro de tuerca: usar PostgreSQL

Para usar PostgreSQL en lugar de SQL Server, sigue estos pasos mínimos:

- **Paquete NuGet a instalar:**

```bash
cd Desafio
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Npgsql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

- **Cambiar la cadena de conexión** en `Utils/DatabaseHelper.cs` (ejemplo):

```csharp
namespace DesafioFinal.Utils
{
   public static class DatabaseHelper
   {
      public static string ConnectionString = "Host=localhost;Port=5432;Database=BibliotecaDesafio;Username=postgres;Password=tu_password";
   }
}
```

- **Configurar el `DbContext`** para que use Npgsql. En `Data/BibliotecaContext.cs` reemplaza `UseSqlServer` por `UseNpgsql`:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
   optionsBuilder.UseNpgsql(DatabaseHelper.ConnectionString);
}
```

- **Al usar ADO.NET manualmente** (archivo `Services/AdoNetService.cs`): cambia `Microsoft.Data.SqlClient` por `Npgsql` y `SqlConnection`/`SqlCommand` por `NpgsqlConnection`/`NpgsqlCommand`. Los parámetros usan `@param` igual que en SQL Server.

- **Migraciones y esquema:** las migraciones generadas para SQL Server no son compatibles con PostgreSQL. Borra la carpeta `Migrations` (si existe) y crea nuevas migraciones.
---

## ⚙️ Preparación del Entorno.

Los proyectos base ya cuentan con la estructura de Arquitectura en Capas (`Models`, `Data`, `Services`, `DTOs`). Para prepararlos en tu máquina, solo asegurate de haber ejecutado los comandos anteriores.

### 📖 Contexto
En proyectos empresariales a gran escala, es común combinar tecnologías. En este desafío, construiremos el motor de una biblioteca. Utilizaremos **ADO.NET** para gestionar a los Usuarios (CRUD simple y rápido) y **EF Core** para gestionar los Libros y la complejidad de los Préstamos, asegurando la integridad de los datos mediante **Transacciones**.

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