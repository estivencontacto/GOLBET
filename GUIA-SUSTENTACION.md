# Guía de sustentación - Segunda entrega

Esta es una estructura de apoyo, no un texto para leer. La guía exige que el estudiante demuestre comprensión personal en una sola toma continua, con pantalla legible y audio claro.

## Antes de grabar

- Verifique que el enlace final pueda abrirse en incógnito.
- Aumente el tamaño del editor hasta que el código sea legible.
- Cierre notificaciones y ventanas con información personal.
- Borre o use una copia limpia de la base si quiere demostrar la migración y el seeder.
- Pruebe micrófono y resolución antes de la toma definitiva.

## Recorrido sugerido (6-7 minutos)

### 0:00-1:30 - Desarrollo realizado

- Muestre `MatchRepository.GetAllWithTeamsAsync` y explique los dos `if` de composición.
- Destaque que `Where` ocurre antes de `ToListAsync`, por lo que SQL filtra los datos.
- Muestre el formulario GET, el campo `search` persistente y los enlaces que conservan ambos parámetros.

### 1:30-2:30 - Capas y por qué

- Repositories: consulta a la base de datos e `Include` de equipos.
- Services: propagación del parámetro y transformación a `MatchDto`.
- Web: model binding del controller y conservación del estado para la vista.
- Explique por qué no se filtra en controller, vista ni memoria.

### 2:30-3:30 - Compilación y ejecución en vivo

- Ejecute `dotnet build GolBet.sln` y muestre 0 errores.
- Ejecute `dotnet run --project GolBet.Web`.
- Abra la URL local y espere a que cargue la cartelera.

### 3:30-6:30 - Prueba funcional

- Busque `nacional` y muestre que aparece como local y visitante.
- Seleccione `Programados` y lea la URL con `status` y `search`.
- Actualice con F5 y confirme que la búsqueda se conserva.
- Seleccione `Finalizados` para mostrar el caso sin resultados y el botón de regreso.
- Limpie la búsqueda, abra un detalle y entre a Administración para demostrar que lo anterior no se rompió.

## Cierre

Resuma en una frase: la búsqueda viaja Web → Services → Repositories, se combina en una sola consulta SQL y vuelve a la vista como DTOs.
