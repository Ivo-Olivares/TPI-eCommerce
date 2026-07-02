USE eCommerce_DB;
GO

-- Marcas demo
IF NOT EXISTS (SELECT 1 FROM MARCAS WHERE Nombre = 'Samsung')
BEGIN
    INSERT INTO MARCAS (Nombre, Activo) VALUES ('Samsung', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM MARCAS WHERE Nombre = 'Apple')
BEGIN
    INSERT INTO MARCAS (Nombre, Activo) VALUES ('Apple', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM MARCAS WHERE Nombre = 'Lenovo')
BEGIN
    INSERT INTO MARCAS (Nombre, Activo) VALUES ('Lenovo', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM MARCAS WHERE Nombre = 'Sony')
BEGIN
    INSERT INTO MARCAS (Nombre, Activo) VALUES ('Sony', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM MARCAS WHERE Nombre = 'JBL')
BEGIN
    INSERT INTO MARCAS (Nombre, Activo) VALUES ('JBL', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM MARCAS WHERE Nombre = 'Puma')
BEGIN
    INSERT INTO MARCAS (Nombre, Activo) VALUES ('Puma', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM MARCAS WHERE Nombre = 'Levis')
BEGIN
    INSERT INTO MARCAS (Nombre, Activo) VALUES ('Levis', 1);
END
GO

-- Productos demo
DECLARE @IdCelulares INT = (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'Celulares');
DECLARE @IdComputadoras INT = (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'Computadoras');
DECLARE @IdAudio INT = (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'Audio');
DECLARE @IdRopa INT = (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'Ropa');

DECLARE @IdSamsung INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'Samsung');
DECLARE @IdApple INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'Apple');
DECLARE @IdLenovo INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'Lenovo');
DECLARE @IdSony INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'Sony');
DECLARE @IdJBL INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'JBL');
DECLARE @IdNike INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'Nike');
DECLARE @IdAdidas INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'Adidas');
DECLARE @IdPuma INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'Puma');
DECLARE @IdLevis INT = (SELECT IdMarca FROM MARCAS WHERE Nombre = 'Levis');

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'CEL-SAM-S24')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdCelulares, @IdSamsung, 'CEL-SAM-S24', 'Samsung Galaxy S24', 'Smartphone Samsung Galaxy S24 con pantalla AMOLED y 256GB.', 1250000, 10, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'CEL-SAM-A55')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdCelulares, @IdSamsung, 'CEL-SAM-A55', 'Samsung Galaxy A55', 'Smartphone Samsung Galaxy A55 con camara triple y 128GB.', 720000, 14, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'CEL-APP-I15')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdCelulares, @IdApple, 'CEL-APP-I15', 'iPhone 15', 'Apple iPhone 15 con pantalla Super Retina y 128GB.', 1600000, 8, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'CEL-APP-I14')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdCelulares, @IdApple, 'CEL-APP-I14', 'iPhone 14', 'Apple iPhone 14 con chip A15 Bionic y 128GB.', 1350000, 6, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'COM-LEN-I5')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdComputadoras, @IdLenovo, 'COM-LEN-I5', 'Lenovo IdeaPad 5', 'Notebook Lenovo IdeaPad 5 con procesador Intel i5 y SSD 512GB.', 980000, 7, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'COM-LEN-THK')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdComputadoras, @IdLenovo, 'COM-LEN-THK', 'Lenovo ThinkPad E14', 'Notebook Lenovo ThinkPad E14 orientada a productividad.', 1350000, 5, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'COM-APP-MBA')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdComputadoras, @IdApple, 'COM-APP-MBA', 'MacBook Air M2', 'Apple MacBook Air con chip M2, 8GB RAM y SSD 256GB.', 2100000, 4, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'COM-APP-MBP')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdComputadoras, @IdApple, 'COM-APP-MBP', 'MacBook Pro 14', 'Apple MacBook Pro de 14 pulgadas para trabajo profesional.', 3600000, 3, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'AUD-SON-WH')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdAudio, @IdSony, 'AUD-SON-WH', 'Sony WH-1000XM5', 'Auriculares Sony inalambricos con cancelacion de ruido.', 520000, 9, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'AUD-SON-XB')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdAudio, @IdSony, 'AUD-SON-XB', 'Sony Extra Bass XB13', 'Parlante portatil Sony Extra Bass compacto y resistente.', 95000, 15, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'AUD-JBL-FLIP')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdAudio, @IdJBL, 'AUD-JBL-FLIP', 'JBL Flip 6', 'Parlante Bluetooth JBL Flip 6 resistente al agua.', 180000, 11, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'AUD-JBL-TUNE')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdAudio, @IdJBL, 'AUD-JBL-TUNE', 'JBL Tune 520BT', 'Auriculares inalambricos JBL Tune con bateria de larga duracion.', 110000, 18, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'ROP-NIK-REM')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdRopa, @IdNike, 'ROP-NIK-REM', 'Remera Nike Sportswear', 'Remera Nike de algodon para uso diario.', 45000, 20, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'ROP-ADI-BUZ')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdRopa, @IdAdidas, 'ROP-ADI-BUZ', 'Buzo Adidas Essentials', 'Buzo Adidas Essentials comodo para entrenamiento y uso urbano.', 95000, 12, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'ROP-PUM-ZAP')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdRopa, @IdPuma, 'ROP-PUM-ZAP', 'Zapatillas Puma Runner', 'Zapatillas Puma Runner livianas para todos los dias.', 135000, 9, 1);

IF NOT EXISTS (SELECT 1 FROM PRODUCTOS WHERE Sku = 'ROP-LEV-JEA')
INSERT INTO PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo)
VALUES (@IdRopa, @IdLevis, 'ROP-LEV-JEA', 'Jean Levis 511', 'Jean Levis 511 corte slim clasico.', 120000, 10, 1);
GO
