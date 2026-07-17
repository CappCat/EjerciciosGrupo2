# NetBdProject

Proyecto guía para practicar los conceptos que aparecen en los ejercicios de la carpeta `Ejercicios`, pero con temática de transacciones bancarias.

## Qué enseña

- CRUD con EF Core sobre `Cuentas` y `Movimientos`.
- CRUD con ADO.NET sobre `Clientes`.
- Uso de DTOs para mostrar información de salida sin exponer las entidades.
- Transacciones de base de datos para transferencias entre cuentas.


## Requisitos

 SQL Server Express en `.\SQLEXPRESS` o una instancia equivalente

## Ejecutar

```bash
cd Codigo/src/NetBdProject
dotnet add package Microsoft.EntityFrameworkCore.SqlServer 
dotnet add package Microsoft.Data.SqlClient
dotnet restore
dotnet run --project NetBdProject.csproj
```

Si tu instancia de SQL Server se llama distinto, cambia la cadena de conexión en `Utils/DatabaseHelper.cs`.