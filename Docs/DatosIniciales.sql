USE eCommerce_DB;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRAN;

    IF COL_LENGTH('PEDIDOS', 'ObservacionesInternas') IS NULL
    BEGIN
        ALTER TABLE PEDIDOS ADD ObservacionesInternas VARCHAR(500) NULL;
    END

    DECLARE @Roles TABLE (Nombre VARCHAR(50) NOT NULL);
    INSERT INTO @Roles (Nombre)
    VALUES ('Cliente'), ('Vendedor'), ('Admin');

    INSERT INTO ROLES (Nombre)
    SELECT R.Nombre
    FROM @Roles R
    WHERE NOT EXISTS (SELECT 1 FROM ROLES WHERE Nombre = R.Nombre);

    DECLARE @Categorias TABLE (Nombre VARCHAR(50) NOT NULL);
    INSERT INTO @Categorias (Nombre)
    VALUES
        ('Tecnologia'),
        ('Hogar'),
        ('Indumentaria'),
        ('Bazar'),
        ('Celulares'),
        ('Computadoras'),
        ('Audio'),
        ('Ropa');

    INSERT INTO CATEGORIAS (Nombre, Activo)
    SELECT C.Nombre, 1
    FROM @Categorias C
    WHERE NOT EXISTS (SELECT 1 FROM CATEGORIAS WHERE Nombre = C.Nombre);

    DECLARE @Marcas TABLE (Nombre VARCHAR(50) NOT NULL);
    INSERT INTO @Marcas (Nombre)
    VALUES
        ('Samsung'),
        ('Apple'),
        ('Lenovo'),
        ('Sony'),
        ('JBL'),
        ('Nike'),
        ('Adidas'),
        ('Puma'),
        ('Levis');

    INSERT INTO MARCAS (Nombre, Activo)
    SELECT M.Nombre, 1
    FROM @Marcas M
    WHERE NOT EXISTS (SELECT 1 FROM MARCAS WHERE Nombre = M.Nombre);

    DECLARE @FormasPago TABLE (Descripcion VARCHAR(50) NOT NULL);
    INSERT INTO @FormasPago (Descripcion)
    VALUES ('Efectivo'), ('Transferencia'), ('Tarjeta'), ('MercadoPago');

    INSERT INTO FORMASPAGO (Descripcion, Activo)
    SELECT FP.Descripcion, 1
    FROM @FormasPago FP
    WHERE NOT EXISTS (SELECT 1 FROM FORMASPAGO WHERE Descripcion = FP.Descripcion);

    DECLARE @FormasEntrega TABLE (Descripcion VARCHAR(50) NOT NULL);
    INSERT INTO @FormasEntrega (Descripcion)
    VALUES ('Retiro en sucursal'), ('Envio a domicilio');

    INSERT INTO FORMASENTREGA (Descripcion, Activo)
    SELECT FE.Descripcion, 1
    FROM @FormasEntrega FE
    WHERE NOT EXISTS (SELECT 1 FROM FORMASENTREGA WHERE Descripcion = FE.Descripcion);

    DECLARE @EstadosPedido TABLE (Descripcion VARCHAR(50) NOT NULL);
    INSERT INTO @EstadosPedido (Descripcion)
    VALUES ('Pendiente'), ('Pagado'), ('En preparacion'), ('Enviado'), ('Entregado'), ('Cancelado');

    INSERT INTO ESTADOSPEDIDO (Descripcion, Activo)
    SELECT EP.Descripcion, 1
    FROM @EstadosPedido EP
    WHERE NOT EXISTS (SELECT 1 FROM ESTADOSPEDIDO WHERE Descripcion = EP.Descripcion);

    DECLARE @ClaveHash VARCHAR(100) = 'PBKDF2$10000$QWRtaW5Mb2NhbFRlc3QhIQ==$s+P9MUai64QBByTHzB8Cyd5TZcpLAoGcCencip6qBlk=';

    IF EXISTS (SELECT 1 FROM USUARIOS WHERE Email = 'admin@admin.com')
    BEGIN
        UPDATE USUARIOS
        SET Nombre = 'Admin',
            Apellido = 'Sistema',
            Dni = '00000000',
            FechaNacimiento = '2000-01-01',
            Telefono = '0000000000',
            Clave = @ClaveHash,
            Activo = 1
        WHERE Email = 'admin@admin.com';
    END
    ELSE
    BEGIN
        INSERT INTO USUARIOS (Nombre, Apellido, Dni, FechaNacimiento, Email, Telefono, Clave, Activo)
        VALUES ('Admin', 'Sistema', '00000000', '2000-01-01', 'admin@admin.com', '0000000000', @ClaveHash, 1);
    END

    IF EXISTS (SELECT 1 FROM USUARIOS WHERE Email = 'vendedor@vendedor.com')
    BEGIN
        UPDATE USUARIOS
        SET Nombre = 'Vendedor',
            Apellido = 'Sistema',
            Dni = '11111111',
            FechaNacimiento = '2000-01-01',
            Telefono = '1111111111',
            Clave = @ClaveHash,
            Activo = 1
        WHERE Email = 'vendedor@vendedor.com';
    END
    ELSE
    BEGIN
        INSERT INTO USUARIOS (Nombre, Apellido, Dni, FechaNacimiento, Email, Telefono, Clave, Activo)
        VALUES ('Vendedor', 'Sistema', '11111111', '2000-01-01', 'vendedor@vendedor.com', '1111111111', @ClaveHash, 1);
    END

    INSERT INTO USUARIOS_ROLES (IdUsuario, IdRol)
    SELECT U.IdUsuario, R.IdRol
    FROM USUARIOS U
    INNER JOIN ROLES R ON R.Nombre = 'Admin'
    WHERE U.Email = 'admin@admin.com'
      AND NOT EXISTS (
          SELECT 1
          FROM USUARIOS_ROLES UR
          WHERE UR.IdUsuario = U.IdUsuario
            AND UR.IdRol = R.IdRol
      );

    INSERT INTO USUARIOS_ROLES (IdUsuario, IdRol)
    SELECT U.IdUsuario, R.IdRol
    FROM USUARIOS U
    INNER JOIN ROLES R ON R.Nombre = 'Vendedor'
    WHERE U.Email = 'vendedor@vendedor.com'
      AND NOT EXISTS (
          SELECT 1
          FROM USUARIOS_ROLES UR
          WHERE UR.IdUsuario = U.IdUsuario
            AND UR.IdRol = R.IdRol
      );

    DECLARE @Productos TABLE (
        Categoria VARCHAR(50) NOT NULL,
        Marca VARCHAR(50) NOT NULL,
        Sku VARCHAR(30) NOT NULL,
        Nombre VARCHAR(100) NOT NULL,
        Descripcion VARCHAR(500) NOT NULL,
        Precio DECIMAL(10,2) NOT NULL,
        Stock INT NOT NULL
    );

    INSERT INTO @Productos (Categoria, Marca, Sku, Nombre, Descripcion, Precio, Stock)
    VALUES
        ('Celulares', 'Samsung', 'CEL-SAM-S24', 'Samsung Galaxy S24', 'Smartphone Samsung Galaxy S24 con pantalla AMOLED y 256GB.', 1250000, 10),
        ('Celulares', 'Samsung', 'CEL-SAM-A55', 'Samsung Galaxy A55', 'Smartphone Samsung Galaxy A55 con camara triple y 128GB.', 720000, 14),
        ('Celulares', 'Apple', 'CEL-APP-I15', 'iPhone 15', 'Apple iPhone 15 con pantalla Super Retina y 128GB.', 1600000, 8),
        ('Celulares', 'Apple', 'CEL-APP-I14', 'iPhone 14', 'Apple iPhone 14 con chip A15 Bionic y 128GB.', 1350000, 6),
        ('Computadoras', 'Lenovo', 'COM-LEN-I5', 'Lenovo IdeaPad 5', 'Notebook Lenovo IdeaPad 5 con procesador Intel i5 y SSD 512GB.', 980000, 7),
        ('Computadoras', 'Lenovo', 'COM-LEN-THK', 'Lenovo ThinkPad E14', 'Notebook Lenovo ThinkPad E14 orientada a productividad.', 1350000, 5),
        ('Computadoras', 'Apple', 'COM-APP-MBA', 'MacBook Air M2', 'Apple MacBook Air con chip M2, 8GB RAM y SSD 256GB.', 2100000, 4),
        ('Computadoras', 'Apple', 'COM-APP-MBP', 'MacBook Pro 14', 'Apple MacBook Pro de 14 pulgadas para trabajo profesional.', 3600000, 3),
        ('Audio', 'Sony', 'AUD-SON-WH', 'Sony WH-1000XM5', 'Auriculares Sony inalambricos con cancelacion de ruido.', 520000, 9),
        ('Audio', 'Sony', 'AUD-SON-XB', 'Sony Extra Bass XB13', 'Parlante portatil Sony Extra Bass compacto y resistente.', 95000, 15),
        ('Audio', 'JBL', 'AUD-JBL-FLIP', 'JBL Flip 6', 'Parlante Bluetooth JBL Flip 6 resistente al agua.', 180000, 11),
        ('Audio', 'JBL', 'AUD-JBL-TUNE', 'JBL Tune 520BT', 'Auriculares inalambricos JBL Tune con bateria de larga duracion.', 110000, 18),
        ('Ropa', 'Nike', 'ROP-NIK-REM', 'Remera Nike Sportswear', 'Remera Nike de algodon para uso diario.', 45000, 20),
        ('Ropa', 'Adidas', 'ROP-ADI-BUZ', 'Buzo Adidas Essentials', 'Buzo Adidas Essentials comodo para entrenamiento y uso urbano.', 95000, 12),
        ('Ropa', 'Puma', 'ROP-PUM-ZAP', 'Zapatillas Puma Runner', 'Zapatillas Puma Runner livianas para todos los dias.', 135000, 9),
        ('Ropa', 'Levis', 'ROP-LEV-JEA', 'Jean Levis 511', 'Jean Levis 511 corte slim clasico.', 120000, 10);

    INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
    SELECT C.IdCategoria, M.IdMarca, P.Sku, P.Nombre, P.Descripcion, P.Precio, P.Stock, 1
    FROM @Productos P
    INNER JOIN CATEGORIAS C ON C.Nombre = P.Categoria
    INNER JOIN MARCAS M ON M.Nombre = P.Marca
    WHERE NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = P.Sku);

    DECLARE @Imagenes TABLE (
        Sku VARCHAR(30) NOT NULL,
        UrlImagen VARCHAR(500) NOT NULL
    );

    INSERT INTO @Imagenes (Sku, UrlImagen)
    VALUES
        ('CEL-SAM-S24', 'https://shopq1.assurancewireless.com/wp-content/uploads/2024/10/Samsung_Galaxy_S24_Black_front.png'),
        ('CEL-SAM-A55', 'https://upload.wikimedia.org/wikipedia/commons/thumb/8/8b/Samsung_Galaxy_A55_5G_2024.jpg/500px-Samsung_Galaxy_A55_5G_2024.jpg'),
        ('CEL-APP-I15', 'https://fdn2.gsmarena.com/vv/pics/apple/apple-iphone-15-1.jpg'),
        ('CEL-APP-I14', 'https://fdn2.gsmarena.com/vv/pics/apple/apple-iphone-14-1.jpg'),
        ('COM-LEN-I5', 'https://www.lenovo.com/medias/lenovo-laptop-ideapad-5-gen-7-15-amd-hero.png'),
        ('COM-LEN-THK', 'https://www.laptoparena.net/images/Lenovo_ThinkPad_E14_21EB001WUS_image_1.jpg'),
        ('COM-APP-MBA', 'https://bizweb.dktcdn.net/thumb/large/100/401/951/products/air-2022-m2-gray-849c9132-630a-46c6-8b25-3dec96f1d8f0.jpg?v=1745075315707'),
        ('COM-APP-MBP', 'https://media.ldlc.com/r705/ld/products/00/06/18/45/LD0006184507_0006184546.jpg'),
        ('AUD-SON-WH', 'https://cdn.mos.cms.futurecdn.net/5yxd9gtYW8Sy2Xd8b68j7a.jpg'),
        ('AUD-SON-XB', 'https://m.media-amazon.com/images/I/71CRQkXfHJL.jpg'),
        ('AUD-JBL-FLIP', 'https://estore.vertexhk.com/wp-content/uploads/2024/02/JBL-FLIP6-WHT.jpg'),
        ('AUD-JBL-TUNE', 'https://m.media-amazon.com/images/I/51QeS0jkx-L.jpg'),
        ('ROP-NIK-REM', 'https://cdn.shopify.com/s/files/1/0603/3031/1875/products/main-square_4c216eb5-4673-4567-9592-da24096d940f_3840x.jpg?v=1695634929'),
        ('ROP-ADI-BUZ', 'https://assets.adidas.com/images/w_600,f_auto,q_auto/dd9507c231d742b2826d817a16035b55_9366/Essentials_Small_Logo_Feel_Cozy_Hoodie_White_IW8183_01_laydown.jpg'),
        ('ROP-PUM-ZAP', 'https://www.tradeinn.com/f/14037/140378221/puma-st-runner-v3-l-superlogo-sneakers.jpg'),
        ('ROP-LEV-JEA', 'https://photos6.spartoo.de/photos/168/16805152/16805152_500_A.jpg');

    INSERT INTO IMAGENES (IdProducto, Nombre, UrlImagen)
    SELECT P.IdProducto, 'Imagen principal', I.UrlImagen
    FROM @Imagenes I
    INNER JOIN PRODUCTOS P ON P.Sku = I.Sku
    WHERE NOT EXISTS (
        SELECT 1
        FROM IMAGENES IMG
        WHERE IMG.IdProducto = P.IdProducto
    );

    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;

    THROW;
END CATCH;
GO

PRINT 'Datos iniciales cargados correctamente.';
GO
