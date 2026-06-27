USE eCommerce_DB;
GO

IF NOT EXISTS (SELECT 1 FROM ROLES WHERE Nombre = 'Vendedor')
    INSERT INTO ROLES (Nombre) VALUES ('Vendedor');
GO

DECLARE @Email VARCHAR(100) = 'vendedor@vendedor.com';
DECLARE @ClaveHash VARCHAR(100) = 'PBKDF2$10000$QWRtaW5Mb2NhbFRlc3QhIQ==$s+P9MUai64QBByTHzB8Cyd5TZcpLAoGcCencip6qBlk=';

IF EXISTS (SELECT 1 FROM USUARIOS WHERE Email = @Email)
BEGIN
    UPDATE USUARIOS
    SET Nombre = 'Vendedor',
        Apellido = 'Sistema',
        Dni = '11111111',
        FechaNacimiento = '2000-01-01',
        Telefono = '1111111111',
        Clave = @ClaveHash,
        Activo = 1
    WHERE Email = @Email;
END
ELSE
BEGIN
    INSERT INTO USUARIOS (Nombre, Apellido, Dni, FechaNacimiento, Email, Telefono, Clave, Activo)
    VALUES ('Vendedor', 'Sistema', '11111111', '2000-01-01', @Email, '1111111111', @ClaveHash, 1);
END

DECLARE @IdUsuario INT = (SELECT IdUsuario FROM USUARIOS WHERE Email = @Email);
DECLARE @IdRol INT = (SELECT IdRol FROM ROLES WHERE Nombre = 'Vendedor');

IF NOT EXISTS (SELECT 1 FROM USUARIOS_ROLES WHERE IdUsuario = @IdUsuario AND IdRol = @IdRol)
    INSERT INTO USUARIOS_ROLES (IdUsuario, IdRol) VALUES (@IdUsuario, @IdRol);

SELECT U.IdUsuario, U.Email, U.Activo, R.Nombre AS RolAsignado
FROM USUARIOS U
INNER JOIN USUARIOS_ROLES UR ON U.IdUsuario = UR.IdUsuario
INNER JOIN ROLES R ON UR.IdRol = R.IdRol
WHERE U.Email = @Email;
GO
