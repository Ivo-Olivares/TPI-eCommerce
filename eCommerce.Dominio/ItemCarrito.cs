using System;

namespace eCommerce.Dominio
{
    [Serializable]
    public class ItemCarrito
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public int IdProducto
        {
            get { return Producto != null ? Producto.Id : 0; }
        }

        public string NombreProducto
        {
            get { return Producto != null ? Producto.Nombre : ""; }
        }

        public int StockDisponible
        {
            get { return Producto != null ? Producto.Stock : 0; }
        }

        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }
    }
}
