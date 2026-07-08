# Guía Práctica: Acceso a Datos en .NET (Grupo 2)

¡Bienvenidos a la práctica de acceso a datos! En esta sesión, exploraremos cómo interactuar con bases de datos en aplicaciones .NET utilizando dos enfoques fundamentales: el moderno **Entity Framework Core (EF Core)** y el tradicional **ADO.NET**.

---

## 🛠️ Requisitos Previos e Instalación

Antes de comenzar a escribir código, asegúrate de tener el siguiente entorno configurado en tu equipo:

1. **.NET SDK:** Instalar la versión más reciente (recomendado .NET 8) desde la página oficial de Microsoft.
2. **Editor de Código:** Visual Studio Code (con la extensión "C# Dev Kit") o Visual Studio 2022.
3. **Motor de Base de Datos:** SQL Server Express (gratuito). Debe estar en ejecución como una instancia local (usualmente bajo el nombre `.\SQLEXPRESS`).
4. **Cliente SQL:** SQL Server Management Studio (SSMS) o Azure Data Studio para visualizar las tablas y registros generados.

---

## ⚙️ Preparación del Entorno (Para ambos ejercicios)

Los proyectos base ya cuentan con la estructura de Arquitectura en Capas (`Models`, `Data`, `Services`, `DTOs`). Para prepararlos en tu máquina, abre tu terminal y ejecuta los siguientes comandos para restaurar las dependencias necesarias.

**Para el Ejercicio 3.3 (EF Core):**
`cd Ejercicio_Clase`
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`

**Para el Desafío 3.4 (Híbrido):**
`cd Desafio`
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
`dotnet add package Microsoft.Data.SqlClient`

> **Nota:** En el archivo `Program.cs` de ambos proyectos se ha incluido el comando `db.Database.EnsureCreated();`. Esto automatiza la creación de la base de datos y las tablas al ejecutar el programa, por lo que no es necesario ejecutar migraciones manualmente para esta clase.