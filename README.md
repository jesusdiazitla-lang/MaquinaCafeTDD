# Máquina de Café — Práctica TDD (ITLA)

## ¿Qué es TDD?

Test-Driven Development (TDD) es una metodología de desarrollo en la que las
pruebas automatizadas se escriben **antes** del código de producción. El ciclo
se repite en pasos cortos:

1. **RED** — Se escribe una prueba que describe el comportamiento deseado.
   La prueba debe fallar, porque el código que la satisface todavía no existe.
2. **GREEN** — Se escribe el código mínimo necesario para que la prueba pase.
   No importa si el diseño es tosco; lo único que importa es que funcione.
3. **REFACTOR** — Se mejora el diseño (se elimina duplicación, se aplican
   buenas prácticas, se renombra, etc.) sin romper ninguna prueba existente.

Este ciclo se repite hasta cubrir toda la funcionalidad requerida.

## Ciclo aplicado en este proyecto

- **RED**: se escribieron los 8 casos de prueba (`TC-01` a `TC-08`) en
  `MaquinaCafe.Tests/MaquinaCafeTests.cs` antes de que existiera la clase
  `MaquinaCafe`. Al compilar por primera vez, todos fallaban (no existía el
  tipo `MaquinaCafe`).
- **GREEN**: se implementó `MaquinaCafe.cs` con la lógica mínima — un
  diccionario simple de precios y un saldo entero — hasta que los 8 tests
  pasaron.
- **REFACTOR**: se introdujo el `record Bebida(Nombre, Precio, Stock)` para
  modelar cada bebida del menú (incluyendo su stock), reemplazando el
  diccionario plano de precios. Se extrajo el método privado
  `ObtenerBebidaOLanzar` para evitar duplicar la validación de existencia.
  Tras el refactor se volvieron a correr los 8 tests y siguieron pasando.

## Casos de prueba cubiertos

| ID | Escenario | Resultado esperado |
|----|-----------|---------------------|
| TC-01 | Insertar monedas | El saldo se acumula |
| TC-02 | Seleccionar bebida con saldo suficiente | `true` |
| TC-03 | Seleccionar bebida con saldo insuficiente | `false` |
| TC-04 | Obtener cambio tras dispensar | Cambio correcto |
| TC-05 | Seleccionar bebida inexistente | `ArgumentException` |
| TC-06 | Consultar el menú | 3 bebidas con sus precios |
| TC-07 | Devolver monedas | Saldo vuelve a 0 |
| TC-08 | Seleccionar bebida sin stock | `false` |

## Estructura del proyecto

```
MaquinaCafeTDD/
├── MaquinaCafe/                 # Proyecto principal (Console App, .NET 8)
│   ├── MaquinaCafe.cs           # Clase MaquinaCafe + record Bebida
│   ├── Program.cs               # Punto de entrada (demo de uso)
│   └── MaquinaCafe.csproj
├── MaquinaCafe.Tests/           # Proyecto de pruebas (NUnit)
│   ├── MaquinaCafeTests.cs      # Los 8 casos de prueba (TC-01 a TC-08)
│   └── MaquinaCafe.Tests.csproj
└── MaquinaCafeTDD.sln
```

## Cómo ejecutar el proyecto

### Desde Visual Studio
1. Abrir `MaquinaCafeTDD.sln`.
2. Para correr la consola: clic derecho en `MaquinaCafe` → **Set as Startup
   Project** → F5.
3. Para correr las pruebas: **Test → Run All Tests** (o `Ctrl+R, A`), y
   verificar que las 8 aparecen en verde en el Test Explorer.

### Desde la línea de comandos (dotnet CLI)

```bash
# Restaurar dependencias
dotnet restore

# Correr la aplicación de consola
dotnet run --project MaquinaCafe

# Correr todas las pruebas
dotnet test
```

Salida esperada de `dotnet test`: **8/8 pruebas pasando, 0 errores, 0
fallos**.

## Notas de diseño

- El menú inicial es: `Cafe = 100`, `Te = 75`, `Agua = 50`, cada uno con
  stock inicial de 10 unidades.
- `Bebida` es un `record` inmutable: al descontar stock se crea una nueva
  instancia (`bebida with { Stock = bebida.Stock - 1 }`) en lugar de mutar el
  objeto original.
- `SeleccionarBebida` valida, en orden: (1) que la bebida exista en el menú
  (si no, lanza `ArgumentException`), (2) que haya saldo suficiente y stock
  disponible (si no, retorna `false`), y solo entonces descuenta saldo y
  stock.
