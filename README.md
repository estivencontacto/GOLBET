# GolBet

Aplicación web para consultar y administrar partidos de fútbol. Permite filtrar por estado, buscar por el nombre de cualquiera de los equipos y revisar la información de cada encuentro.

## Tecnologías

- ASP.NET Core MVC con .NET 8
- Entity Framework Core y SQL Server
- Razor, Bootstrap y AutoMapper

## Estructura

- `GolBet.Entities`: entidades y enumeraciones del dominio.
- `GolBet.Repositories`: acceso a datos, migraciones y consultas.
- `GolBet.Services`: lógica de negocio, DTOs y mapeos.
- `GolBet.Web`: controllers, vistas y archivos estáticos.

## Cómo ejecutar

Se necesita .NET 8 y SQL Server LocalDB. Si se usa otra instancia, hay que cambiar la cadena `GolBetConnection` en `GolBet.Web/appsettings.json`.

```powershell
dotnet restore GolBet.sln
dotnet build GolBet.sln
dotnet run --project GolBet.Web
```

Al iniciar por primera vez se crea la base de datos y se cargan los datos de prueba. El proceso puede ejecutarse de nuevo sin duplicarlos.

## Funciones

- Listado y detalle de partidos.
- Filtros por estado.
- Búsqueda por equipo local o visitante.
- Combinación de búsqueda y filtros en la misma consulta.
- Creación y edición de partidos desde el panel de administración.
- Página 404 para partidos inexistentes.
