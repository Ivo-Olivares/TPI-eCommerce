# Setup local

Esta guia resume los pasos para levantar el proyecto en una computadora local y dejar la base lista para pruebas.

## Requisitos

- SQL Server local con instancia `.\SQLEXPRESS`.
- Windows Authentication habilitado.
- Visual Studio 2022 o Build Tools con soporte para ASP.NET Web Forms / .NET Framework.
- Base de datos local llamada `eCommerce_DB`.

La cadena usada por el proyecto esta en `eCommerce.Web\Web.config`:

```xml
server=.\SQLEXPRESS; database=eCommerce_DB; integrated security=true
```

## Base nueva

Si todavia no existe la base `eCommerce_DB`, ejecutar desde la raiz del repo:

```powershell
sqlcmd -S .\SQLEXPRESS -E -i Docs\eCommerce_DB.sql
sqlcmd -S .\SQLEXPRESS -E -i Docs\DatosIniciales.sql
```

## Base existente

Para evitar inconsistencias entre versiones viejas de la base y el codigo actual, lo recomendado para una prueba local limpia es recrear la base y ejecutar los dos scripts anteriores.

## Usuarios locales

El script `Docs\DatosIniciales.sql` crea o actualiza estos usuarios:

- Email: `admin@admin.com`
- Clave: `admin`
- Rol: `Admin`

- Email: `vendedor@vendedor.com`
- Clave: `admin`
- Rol: `Vendedor`

El script tambien carga roles, categorias, marcas, formas de pago, formas de entrega, estados de pedido, productos demo e imagenes. Es idempotente: se puede ejecutar mas de una vez sin duplicar datos. Si la base local ya existia, tambien agrega la columna `ObservacionesInternas` en `PEDIDOS` cuando falta.

## Validar base local

Despues de ejecutar los scripts, validar el setup con:

```powershell
sqlcmd -S .\SQLEXPRESS -E -i Docs\VerificarSetupLocal.sql
```

La verificacion debe confirmar las tablas principales, los roles base, usuarios locales, datos administrables, productos e imagenes.

## Compilar el proyecto

Este proyecto es ASP.NET Web Forms con .NET Framework. Usar MSBuild de Visual Studio, no `dotnet build`:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe' eCommerce.slnx /t:Build /p:Configuration=Debug /p:Platform='Any CPU' /v:minimal
```

## Probar en navegador

1. Levantar el sitio desde Visual Studio o IIS Express.
2. Abrir `https://localhost:44374/`.
3. Ingresar con `admin@admin.com` / `admin`.
4. Confirmar que se ve `Panel de Administracion`.
5. Abrir un ABM administrable para confirmar que la aplicacion responde contra la base local.
