using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrProyectoFinal
{
    internal class Carrito
    {
        //Con esta lista almacenamos los productos que vamos a mostrar en el carrito
        public List<Producto> Productos { get; set; }
        public double precioTotal = 0;

        //La instaciamos cuando creamos el objeto
        public Carrito()
        {
            Productos = new List<Producto>();
        }

        //Con este metodo agregamos los productos que queremos poner al carrito 
        public void AgregarAlCarrito(Producto producto)
        {
            Productos.Add(producto);
        }

        //public void CalcularPrecioTotal(List<Producto> Productos)
        //{
        //   // double precioTotalProducto;
        //    foreach (Producto producto in Productos)
        //    {
        //        //precioTotalProducto = producto.Cantidad * precioTotal;
        //        precioTotal += producto.Precio;
        //    }
        //}

        public double CalcularPrecioTotal(List<Producto> productos)
        {
            double precioTotal = 0.0;

            foreach (Producto producto in productos)
            {
                double cantidad = producto.Cantidad;
                double precio = producto.Precio;

                // Añadir el precio del producto multiplicado por la cantidad al precio total
                precioTotal += cantidad * precio;
            }

            return precioTotal;
        }


    }
}
