# -- Modelo de Dominio - eCommerce --

-------------------------------------------------

# Entidades
- Producto
- Categoria
- Marca
- Imagen
- Usuario
- Direccion
- Pedido
- DetallePedido
- EstadoPedido
- FormaPago
- FormaEntrega

-------------------------------------------------

# Relaciones

## Pedido
- Usuario 1 ---- * Pedido
- Pedido 1 ---- 1 Direccion
- Pedido 1 ---- * DetallePedido
- Pedido 1 ---- 1 EstadoPedido
- Pedido 1 ---- 1 FormaPago
- Pedido 1 ---- 1 FormaEntrega

## Producto
- Producto 1 ---- * DetallePedido
- Producto 1 ---- * Imagen
- Categoria 1 ---- * Producto
- Marca 1 ---- * Producto

## Usuario
- Usuario 1 ---- * Direccion

-------------------------------------------------