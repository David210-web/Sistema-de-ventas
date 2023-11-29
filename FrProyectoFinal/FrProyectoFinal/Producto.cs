using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrProyectoFinal
{
     public class Producto
    {
        //El producto tendra estos atributos
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int cantidad = 1;

        public int Cantidad {
            get { return cantidad; }
            set { cantidad = value; } 
        }

        //Creo un constructor
        public Producto(int id, string nombre,string descripcion, double precio,int cantidad)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Cantidad = cantidad;
        }
        //Lo sobrecargo (este servira para el carrito de compras ya que la cantidad no sera fija y tendra que incrementarse y decrementarse)
        public Producto(int id, string nombre,string descripcion, double precio)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
        }
    }
}
