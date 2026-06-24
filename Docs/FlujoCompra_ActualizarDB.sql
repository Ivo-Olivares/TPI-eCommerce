USE eCommerce_DB;
GO

IF NOT EXISTS (SELECT 1 FROM ESTADOSPEDIDO WHERE Descripcion = 'Pendiente')
    INSERT INTO ESTADOSPEDIDO (Descripcion, Activo) VALUES ('Pendiente', 1);

IF NOT EXISTS (SELECT 1 FROM ESTADOSPEDIDO WHERE Descripcion = 'Pagado')
    INSERT INTO ESTADOSPEDIDO (Descripcion, Activo) VALUES ('Pagado', 1);

IF NOT EXISTS (SELECT 1 FROM ESTADOSPEDIDO WHERE Descripcion = 'En preparacion')
    INSERT INTO ESTADOSPEDIDO (Descripcion, Activo) VALUES ('En preparacion', 1);

IF NOT EXISTS (SELECT 1 FROM ESTADOSPEDIDO WHERE Descripcion = 'Enviado')
    INSERT INTO ESTADOSPEDIDO (Descripcion, Activo) VALUES ('Enviado', 1);

IF NOT EXISTS (SELECT 1 FROM ESTADOSPEDIDO WHERE Descripcion = 'Entregado')
    INSERT INTO ESTADOSPEDIDO (Descripcion, Activo) VALUES ('Entregado', 1);

IF NOT EXISTS (SELECT 1 FROM ESTADOSPEDIDO WHERE Descripcion = 'Cancelado')
    INSERT INTO ESTADOSPEDIDO (Descripcion, Activo) VALUES ('Cancelado', 1);

UPDATE ESTADOSPEDIDO
SET Activo = 1
WHERE Descripcion IN ('Pendiente', 'Pagado', 'En preparacion', 'Enviado', 'Entregado', 'Cancelado');
GO
