USE eCommerce_DB;
GO

SET NOCOUNT ON;

PRINT 'Verificando tablas principales...';

DECLARE @TablasRequeridas TABLE (Nombre VARCHAR(50) NOT NULL);

INSERT INTO @TablasRequeridas (Nombre)
VALUES
    ('USUARIOS'),
    ('ROLES'),
    ('USUARIOS_ROLES'),
    ('CATEGORIAS'),
    ('MARCAS'),
    ('PRODUCTOS'),
    ('PEDIDOS'),
    ('DETALLESPEDIDO');

SELECT
    TReq.Nombre AS Tabla,
    CASE WHEN T.TABLE_NAME IS NULL THEN 'FALTA' ELSE 'OK' END AS Estado
FROM @TablasRequeridas TReq
LEFT JOIN INFORMATION_SCHEMA.TABLES T
    ON T.TABLE_NAME = TReq.Nombre
    AND T.TABLE_TYPE = 'BASE TABLE';

IF EXISTS (
    SELECT 1
    FROM @TablasRequeridas TReq
    LEFT JOIN INFORMATION_SCHEMA.TABLES T
        ON T.TABLE_NAME = TReq.Nombre
        AND T.TABLE_TYPE = 'BASE TABLE'
    WHERE T.TABLE_NAME IS NULL
)
BEGIN
    RAISERROR('Faltan tablas requeridas para el setup local.', 16, 1);
    RETURN;
END

PRINT 'Verificando roles base...';

DECLARE @RolesRequeridos TABLE (Nombre VARCHAR(50) NOT NULL);

INSERT INTO @RolesRequeridos (Nombre)
VALUES
    ('Cliente'),
    ('Vendedor'),
    ('Admin');

SELECT
    RReq.Nombre AS Rol,
    CASE WHEN R.Nombre IS NULL THEN 'FALTA' ELSE 'OK' END AS Estado
FROM @RolesRequeridos RReq
LEFT JOIN ROLES R
    ON R.Nombre = RReq.Nombre;

IF EXISTS (
    SELECT 1
    FROM @RolesRequeridos RReq
    LEFT JOIN ROLES R
        ON R.Nombre = RReq.Nombre
    WHERE R.Nombre IS NULL
)
BEGIN
    RAISERROR('Faltan roles base para el setup local.', 16, 1);
    RETURN;
END

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
    RETURN;
END
ELSE
BEGIN
    PRINT 'Setup local verificado correctamente.';
END
GO
