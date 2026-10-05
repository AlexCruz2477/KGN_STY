# KGN_STY Funcional

Versión de **KGN_STY** adaptada para trabajar con la misma base de datos de **NK Collection**, manteniendo la estructura visual y de formularios propia de KGN_STY.

## Qué se integró

- Autenticación y recuperación/restablecimiento de contraseña.
- Apertura, resumen, arqueo, egresos y cierre de caja.
- Ventas con métodos de pago y soporte de tasa de cambio.
- Compras.
- Clientes, proveedores, usuarios y catálogos.
- Productos, variantes, tallas, colores, marcas y categorías.
- Resumen principal con ventas, compras, stock bajo y estado de caja.
- Mantenimiento con respaldo/restauración de PostgreSQL.
- Modelos y `DbContext` alineados con la BD `NK_COLLECTION`.
- Integración preparada para los triggers de inventario/auditoría de PostgreSQL, evitando duplicar movimientos de stock desde C#.

## Base de datos

Por defecto el proyecto usa:

```text
Host=localhost;Port=5432;Database=NK_COLLECTION;Username=Ari;Password=12345
```

La cadena puede cambiarse sin tocar el código creando la variable de entorno:

```text
NK_COLLECTION_CONNECTION
```

Ejemplo en Windows PowerShell:

```powershell
$env:NK_COLLECTION_CONNECTION='Host=localhost;Port=5432;Database=NK_COLLECTION;Username=postgres;Password=TU_CLAVE'
```

En la carpeta `BaseDatos` se incluyen como referencia los scripts suministrados:

- `NK_COPIA_CON_DATOS_REALISTAS.sql`
- `Indices_CORREGIDOS.sql`
- `Triggers_CORREGIDOS.sql`

Si tu BD ya contiene esas estructuras, **no vuelvas a ejecutar los scripts a ciegas**.

## Cómo abrirlo

1. Abre `Nk_Colletion_New.sln` en Visual Studio 2022.
2. Verifica que tengas instalado el workload **Desarrollo de escritorio con .NET**.
3. El proyecto apunta a **.NET 8 para Windows**.
4. Restaura los paquetes NuGet.
5. Verifica la conexión a PostgreSQL.
6. Ejecuta el proyecto.

## Paquetes principales

- Entity Framework Core 8
- Npgsql / Npgsql.EntityFrameworkCore.PostgreSQL 8
- Guna.UI2.WinForms
- MailKit

## Nota de validación

La estructura y referencias del código fueron revisadas estáticamente en el entorno de entrega. Este entorno no dispone del SDK de .NET/WinForms para ejecutar una compilación real de Windows, por lo que la comprobación final debe hacerse al abrir la solución en Visual Studio.
