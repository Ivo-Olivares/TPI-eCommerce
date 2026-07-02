# TPI Programacion 3 - eCommerce

Trabajo Practico Integrador de Programacion III desarrollado por el Equipo 11A de UTN FRGP.

El proyecto es una aplicacion eCommerce construida con ASP.NET Web Forms y .NET Framework. Permite administrar el catalogo y las entidades principales del sistema, registrar usuarios, iniciar sesion y recorrer el flujo base de compra previsto para el TPI.

## Stack

- ASP.NET Web Forms
- .NET Framework
- SQL Server
- ADO.NET
- Bootstrap

## Funcionalidades principales

- Catalogo de productos.
- ABM de productos, categorias, marcas, formas de pago, formas de entrega y estados de pedido.
- Baja logica en entidades administrables.
- Registro e inicio de sesion de usuarios.
- Usuario administrador local para pruebas.
- Pantallas base de carrito, checkout, perfil y compras.

## Estructura del proyecto

- `eCommerce.Dominio`: modelos de dominio.
- `eCommerce.Datos`: acceso a base de datos.
- `eCommerce.Negocio`: reglas de negocio y validaciones.
- `eCommerce.Web`: aplicacion Web Forms.
- `Docs`: scripts SQL, DER, etapas del TPI y guias de setup.

## Setup local

La guia completa para levantar la base local, cargar datos iniciales y validar el entorno esta en [Docs/SetupLocal.md](Docs/SetupLocal.md).

Credenciales locales de prueba:

- Email: `admin@admin.com`
- Clave: `admin`
- Email: `vendedor@vendedor.com`
- Clave: `admin`

Validacion rapida de base local:

```powershell
sqlcmd -S .\SQLEXPRESS -E -i Docs\VerificarSetupLocal.sql
```

Compilacion recomendada:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe' eCommerce.slnx /t:Build /p:Configuration=Debug /p:Platform='Any CPU' /v:minimal
```

## Documentacion

- [Setup local](Docs/SetupLocal.md)
- [Etapas del TPI](Docs/EtapasTPI.md)
- [Modelo de dominio](Docs/ModeloDominio.md)
- [Script de creacion de base](Docs/eCommerce_DB.sql)
- [Script de carga de datos iniciales](Docs/DatosIniciales.sql)

## Equipo 11A

- Axel Sanz
- Ivan Gabriel Olivares
- Lucas Alejo Bellesi
