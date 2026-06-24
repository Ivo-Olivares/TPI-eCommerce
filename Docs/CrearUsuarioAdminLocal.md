# Crear usuario admin local

Este script crea o actualiza un usuario administrador para pruebas locales.

Credenciales:

- Email: `admin@admin.com`
- Clave: `admin`

## Requisito previo

La base local debe tener las tablas de autenticacion creadas. Si todavia no las tenes, ejecuta primero:

```powershell
sqlcmd -S .\SQLEXPRESS -E -i Docs\AutenticacionUsuarios_ActualizarDB.sql
sqlcmd -S .\SQLEXPRESS -E -i Docs\LimpiezaRoles_ActualizarDB.sql
```

## Ejecutar el script

Desde la raiz del repo:

```powershell
sqlcmd -S .\SQLEXPRESS -E -i Docs\CrearUsuarioAdminLocal.sql
```

Tambien se puede abrir `Docs\CrearUsuarioAdminLocal.sql` en SQL Server Management Studio y ejecutarlo sobre la base `eCommerce_DB`.

El script es idempotente: si el usuario ya existe, actualiza sus datos, lo deja activo y asegura que tenga el rol `Admin`.
