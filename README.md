# EjerciciosGrupo2

Resumen breve del repositorio y su estructura.

## Estructura del repositorio

- `Codigo/` — Proyecto principal y material de exposición
  - `src/NetBdProject/` — Proyecto .NET de ejemplo (Models, Data, Services, Utils)

- `Desafio/` — Proyecto del desafío (principalmente para entregar y probar)
  - `Desafio.csproj` — Proyecto .NET
  - `Guia.md` — Guía práctica con instrucciones de ejecución
  - `Data/` — `BibliotecaContext.cs` (DbContext de EF Core)
  - `Services/` — `AdoNetService.cs`, `EfCoreService.cs` (implementaciones de acceso a datos)
  - `Utils/DatabaseHelper.cs` — Cadena de conexión centralizada
  - `DTOs/`, `Models/` — carpetas relacionadas con el modelo

- `Documentos` — carpeta con la presentación y trabajo teóricos requeridos
