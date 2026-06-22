USE eCommerce_DB;
GO

IF NOT EXISTS (SELECT 1 FROM ROLES WHERE Nombre = 'Admin')
    INSERT INTO ROLES (Nombre) VALUES ('Admin');
GO

DECLARE @Email VARCHAR(100) = 'admin@admin.com';
DECLARE @ClaveHash VARCHAR(100) = 'PBKDF2$10000$QWRtaW5Mb2NhbFRlc3QhIQ==$s+P9MUai64QBByTHzB8Cyd5TZcpLAoGcCencip6qBlk=';

IF EXISTS (SELECT 1 FROM USUARIOS WHERE Email = @Email)
BEGIN
    UPDATE USUARIOS
    SET Nombre = 'Admin',
        Apellido = 'Sistema',
        Dni = '00000000',
        FechaNacimiento = '2000-01-01',
        Telefono = '0000000000',
        Clave = @ClaveHash,
        Rol = 'Admin',
        Activo = 1
    WHERE Email = @Email;
END
ELSE
BEGIN
    INSERT INTO USUARIOS (Nombre, Apellido, Dni, FechaNacimiento, Email, Telefono, Clave, Rol, Activo)
    VALUES ('Admin', 'Sistema', '00000000', '2000-01-01', @Email, '0000000000', @ClaveHash, 'Admin', 1);
END

DECLARE @IdUsuario INT = (SELECT IdUsuario FROM USUARIOS WHERE Email = @Email);
DECLARE @IdRol INT = (SELECT IdRol FROM ROLES WHERE Nombre = 'Admin');

IF NOT EXISTS (SELECT 1 FROM USUARIOS_ROLES WHERE IdUsuario = @IdUsuario AND IdRol = @IdRol)
    INSERT INTO USUARIOS_ROLES (IdUsuario, IdRol) VALUES (@IdUsuario, @IdRol);

SELECT U.IdUsuario, U.Email, U.Rol, U.Activo, R.Nombre AS RolAsignado
FROM USUARIOS U
INNER JOIN USUARIOS_ROLES UR ON U.IdUsuario = UR.IdUsuario
INNER JOIN ROLES R ON UR.IdRol = R.IdRol
WHERE U.Email = @Email;
GO
