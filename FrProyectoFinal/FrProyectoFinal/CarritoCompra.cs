using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrProyectoFinal
{
    public partial class CarritoCompra : Form
    {
        //Creamos una lista para almacenar todos los productos que hemos seleccionado y mostrarlo en el carrito de compras
        private List<Producto> Productos { get; set; }
        private Carrito carrito = new Carrito();

        /*Al constructor le pasamos la lista de productos guardados para poner en el carrito
         en el formulario principal
         */
        public CarritoCompra(List<Producto> productos)
        {
            InitializeComponent();
            this.Productos = productos;
        }

        //Cuando el formulario carge se mostrara lo siguiente
        private void CarritoCompra_Load(object sender, EventArgs e)
        {
            //Si la lista de productos no esta vacia nos mostrara la tabla y ocultara el label que dice "No hay datos"
            MostrarDatos();
        }

        //public void MostrarDatos()
        //{
        //    //Si la lista de productos tiene uno o mas objetos la mostrara y ocultara el label
        //    if (Productos.Count > 0)
        //    {
        //        VerCarrito();
        //        pnCarritolleno.Visible = true;
        //        lblCarritoVacio.Visible = false;
        //       carrito.CalcularPrecioTotal(Productos);
        //        lblPrecioTotal.Text = carrito.precioTotal.ToString();

        //    }
        //    else //Pero si no hay datos en la lista no se mostrara la tabla y mostrara un label que dice "No hay datos"
        //    {
        //        lblCarritoVacio.Visible = true;
        //        pnCarritolleno.Visible = false;
        //    }
        //}
        public void MostrarDatos()
        {
            if (Productos.Count > 0)
            {
                VerCarrito();
                pnCarritolleno.Visible = true;
                lblCarritoVacio.Visible = false;

                // Calcular el precio total
                double precioTotal = carrito.CalcularPrecioTotal(Productos);
                lblPrecioTotal.Text = precioTotal.ToString();
            }
            else
            {
                lblCarritoVacio.Visible = true;
                pnCarritolleno.Visible = false;
            }
        }

        public void VerCarrito()
        {
            //Mando la lista al datagrid 
            dgvCarrito.DataSource = Productos;
            //Hago que los campos ocupen todo el ancho del datagrid sino se mirara todo topado y se vera feo
            foreach (DataGridViewColumn column in dgvCarrito.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            double precioTotal = Convert.ToDouble(lblPrecioTotal.Text);
            Compra compra = new Compra(precioTotal);
            Hide();
            compra.ShowDialog();
            Show();
        }

        //Creamos el metodo eliminar productos
        private void EliminarProducto(int rowIndex)
        {
            try
            {
                //Con el parametro rowIndex seleccionamos la fila que queremos eliminar
                // y la guardamos en el objeto productoSeleccionado
                Producto productoSeleccionado = Productos[rowIndex];

                // Eliminar el producto de la lista
                Productos.Remove(productoSeleccionado);

                //Vaciamos la tabla dal carrito de compras para evitar errores
                dgvCarrito.DataSource = null;
                // la volvemos a mostrar con la lista modificada con el metodo mostrar datos
                MostrarDatos();

            }
            catch (Exception )
            {
                //Si hay algun error mostrara esta alerta
                MessageBox.Show("Ha habido un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void AumentoCantidad()
        //{
        //    // Obtenenmos la fila seleccionada en el DataGridView
        //    int rowIndex = dgvCarrito.CurrentCell.RowIndex;

        //    // Asegurarse de que haya una fila seleccionada
        //    if (rowIndex >= 0)
        //    {
        //        // Obtener el producto de la lista según la fila seleccionada
        //        Producto productoSeleccionado = Productos[rowIndex];
        //       // Si la cantidad es mayor que 0 y menos que 10 haremos que la cantidad aumente si no 
        //       // no hara nada
        //        if (productoSeleccionado.Cantidad > 0 && productoSeleccionado.Cantidad < 10)
        //        {
        //           //Cuando aumentamos la cantidad la tabla se refrescar para poder ver los datos
        //            productoSeleccionado.Cantidad++;
        //            dgvCarrito.Refresh();

        //            carrito.CalcularPrecioTotal(Productos,1);
        //            lblPrecioTotal.Text = carrito.precioTotal.ToString();

        //        }
        //    }
        //}
        private void AumentoCantidad()
        {
            int rowIndex = dgvCarrito.CurrentCell.RowIndex;

            if (rowIndex >= 0)
            {
                Producto productoSeleccionado = Productos[rowIndex];

                if (productoSeleccionado.Cantidad > 0 && productoSeleccionado.Cantidad < 10)
                {
                    productoSeleccionado.Cantidad++;
                    dgvCarrito.Refresh();

                    //calcular precio total y mostrar lo
                    double precioTotal = carrito.CalcularPrecioTotal(Productos);
                    lblPrecioTotal.Text = precioTotal.ToString();
                }
            }
        }


        //private void DecrementoCantidad()
        // {
        //     // Obtener la fila seleccionada en el DataGridView
        //     int rowIndex = dgvCarrito.CurrentCell.RowIndex;

        //     // Asegurarse de que haya una fila seleccionada
        //     if (rowIndex >= 0)
        //     {
        //         // Obtener el producto de la lista según la fila seleccionada
        //         Producto productoSeleccionado = Productos[rowIndex];

        //         if (productoSeleccionado.Cantidad > 1)
        //         {
        //             productoSeleccionado.Cantidad--;
        //             dgvCarrito.Refresh();

        //             carrito.CalcularPrecioTotal(Productos,2);
        //             lblPrecioTotal.Text = carrito.precioTotal.ToString();

        //         }


        //     }
        // }
        private void DecrementoCantidad()
        {
            int rowIndex = dgvCarrito.CurrentCell.RowIndex;

            if (rowIndex >= 0)
            {
                Producto productoSeleccionado = Productos[rowIndex];

                if (productoSeleccionado.Cantidad > 1)
                {
                    productoSeleccionado.Cantidad--;
                    dgvCarrito.Refresh();

                    double precioTotal = carrito.CalcularPrecioTotal(Productos);
                    lblPrecioTotal.Text = precioTotal.ToString();
                }
            }
        }

        private void dgvCarrito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Si seleccionamos el boton eliminar de la tabla llamara al metodo eliminar productos
            if (e.RowIndex >= 0 && e.RowIndex < Productos.Count && e.ColumnIndex == dgvCarrito.Columns["Eliminar"].Index)
                EliminarProducto(e.RowIndex);
            //Si seleccionamos el boton aumentar de la tabla llamara al metodo Aumento cantidad
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCarrito.Columns["Aumentar"].Index)
                AumentoCantidad();
            //Si seleccionamos el boton decrementar de la tabla llamara al metodo Decremento cantidad
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCarrito.Columns["Decrementar"].Index)
                DecrementoCantidad();
        }
    }

}
