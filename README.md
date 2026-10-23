# GolBet

Proyecto académico en ASP.NET Core MVC (.NET 8) organizado en arquitectura N-Capas. Implementa la primera entrega (módulos 1 al 5), la base administrativa esperada hasta el módulo 7 y el taller de la segunda entrega: búsqueda de partidos por equipo combinada con filtros de estado.

## Requisitos

- .NET 8 SDK o un SDK posterior capaz de compilar `net8.0`.
- SQL Server LocalDB (incluido normalmente con Visual Studio) o una instancia de SQL Server.
- Si no usa LocalDB, cambie `ConnectionStrings:GolBetConnection` en `GolBet.Web/appsettings.json`.

## Ejecutar

```powershell
dotnet restore GolBet.sln
dotnet build GolBet.sln
dotnet run --project GolBet.Web
```

En el primer arranque, Entity Framework aplica la migración, crea `GolBetDB` y ejecuta un seeder idempotente con 8 equipos y 6 partidos. Ejecutar la aplicación otra vez no duplica los datos.

## Arquitectura

- `GolBet.Entities`: entidades `Team`, `Match` y `Bet`, clase base auditable y enums.
- `GolBet.Repositories`: `DbContext`, migración, repositorio genérico, consultas de partidos y seeder.
- `GolBet.Services`: DTOs planos, `MappingProfile` y reglas de aplicación.
- `GolBet.Web`: controllers delgados, vistas Razor, cartelera y administración.

El buscador de la segunda entrega se compone en `MatchRepository` antes de `ToListAsync`; por tanto, nombre local/visitante, estado y texto se filtran en SQL. El servicio propaga los parámetros, el controller conserva el estado mediante `ViewBag` y la vista usa un formulario GET.

## Pruebas manuales de aceptación

1. Abra `/Matches`: deben aparecer 6 partidos y cuatro filtros.
2. Busque `nacional`: deben aparecer dos partidos, uno como local y otro como visitante.
3. Conserve la búsqueda y seleccione `Programados`: la URL debe incluir `status=Scheduled&search=nacional`.
4. Seleccione `Finalizados`: debe mostrarse el estado vacío con enlace para volver.
5. Abra un detalle y confirme breadcrumb, escudos, cuotas nombradas, contador y botón del módulo 8.
6. Abra `/Matches/Detail/999`: debe responder HTTP 404.
7. Abra `/AdminMatches` y pruebe crear/editar un partido válido.
8. Reinicie la aplicación y confirme que siguen existiendo 8 equipos sin duplicados.

## Historial requerido

El repositorio incluye los tags `modulo-1` a `modulo-7`. Los commits y tags creados aquí tienen fechas reales: no se alteró el historial para simular semanas anteriores. Antes de entregar, el estudiante debe crear su propio repositorio público, configurar el remoto y subir `main` y los tags.

## Segunda entrega

La implementación está lista, pero el video debe grabarlo el estudiante con su propia voz y comprensión. Consulte `GUIA-SUSTENTACION.md` para preparar una toma continua de 5 a 8 minutos.
