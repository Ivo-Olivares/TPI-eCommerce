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
sqlcmd -S .\SQLEXPRESS -E -i Docs\CrearUsuarioAdminLocal.sql
```

## Base existente

Si la base ya existia antes de los cambios de autenticacion, ejecutar:

```powershell
sqlcmd -S .\SQLEXPRESS -E -i Docs\AutenticacionUsuarios_ActualizarDB.sql
sqlcmd -S .\SQLEXPRESS -E -i Docs\LimpiezaRoles_ActualizarDB.sql
sqlcmd -S .\SQLEXPRESS -E -i Docs\CrearUsuarioAdminLocal.sql
```

## Usuario admin local

El script `Docs\CrearUsuarioAdminLocal.sql` crea o actualiza este usuario:

- Email: `admin@admin.com`
- Clave: `admin`
- Rol: `Admin`

El script es idempotente. Se puede ejecutar mas de una vez sin duplicar el usuario ni el rol.

## Validar base local

Despues de ejecutar los scripts, validar el setup con:

```powershell
sqlcmd -S .\SQLEXPRESS -E -i Docs\VerificarSetupLocal.sql
```

La verificacion debe confirmar las tablas principales, los roles base y el usuario admin activo.

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
