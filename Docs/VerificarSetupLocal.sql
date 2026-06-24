USE eCommerce_DB;
GO

PRINT 'Verificando tablas principales...';

SELECT
    V.Tabla,
    CASE WHEN T.TABLE_NAME IS NULL THEN 'FALTA' ELSE 'OK' END AS Estado
FROM (VALUES
    ('USUARIOS'),
    ('ROLES'),
    ('USUARIOS_ROLES'),
    ('CATEGORIAS'),
    ('MARCAS'),
    ('PRODUCTOS'),
    ('PEDIDOS'),
    ('DETALLESPEDIDO')
) AS V(Tabla)
LEFT JOIN INFORMATION_SCHEMA.TABLES T
    ON T.TABLE_NAME = V.Tabla
    AND T.TABLE_TYPE = 'BASE TABLE';

PRINT 'Verificando roles base...';

SELECT
    V.Rol,
    CASE WHEN R.Nombre IS NULL THEN 'FALTA' ELSE 'OK' END AS Estado
FROM (VALUES
    ('Cliente'),
    ('Vendedor'),
    ('Admin')
) AS V(Rol)
LEFT JOIN ROLES R
    ON R.Nombre = V.Rol;

PRINT 'Verificando usuario admin local...';

SELECT
    U.Email,
    U.Activo,
    CASE WHEN R.Nombre = 'Admin' THEN 'OK' ELSE 'FALTA ROL ADMIN' END AS EstadoRol
FROM USUARIOS U
LEFT JOIN USUARIOS_ROLES UR ON U.IdUsuario = UR.IdUsuario
LEFT JOIN ROLES R ON UR.IdRol = R.IdRol AND R.Nombre = 'Admin'
WHERE U.Email = 'admin@admin.com';

IF NOT EXISTS (
    SELECT 1
    FROM USUARIOS U
    INNER JOIN USUARIOS_ROLES UR ON U.IdUsuario = UR.IdUsuario
    INNER JOIN ROLES R ON UR.IdRol = R.IdRol
    WHERE U.Email = 'admin@admin.com'
      AND U.Activo = 1
      AND R.Nombre = 'Admin'
)
BEGIN
    RAISERROR('El usuario admin@admin.com no existe, no esta activo o no tiene rol Admin.', 16, 1);
END
ELSE
BEGIN
    PRINT 'Setup local verificado correctamente.';
END
GO
