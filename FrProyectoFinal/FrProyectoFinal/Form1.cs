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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private Conexion conexion = new Conexion();
        private Carrito carrito = new Carrito();


        //Con este metodo hago que la tabla pueda mostrar los elementos de la lista obtenida de la clase conexion (con el select)
        //Ahora lleva un parametro de tipo lista
        private void Refrescar(List<Producto> productos)
        {
            tablaProductos.DataSource = productos ;
            foreach (DataGridViewColumn column in tablaProductos.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        //Si en la barra de busqueda no hay nada, nos cargara automaticamente la base de datos con todos los productos
        private void CargarProductos(string nombreBuscar = null)
        {
            List<Producto> productos;
            //Si el nombreBuscar no tiene ningun valor entonces en la lista se guardara todos los productos
            if (nombreBuscar == null)
                productos = conexion.MostrarProductos();
            else //Si nombreBuscar tiene un texto entonces guardara los productos que tengan coincidencia en el nombre en la lista
                productos = conexion.BuscarProductosNombre(nombreBuscar);

            Refrescar(productos);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            //Al momento de cargar el formulario se ejecutara el metodo que guarda la tabla con la lista
            CargarProductos();
        }

        

        private void tablaProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Al darle click al boton "Agregar" que esta en la tabla
            /*
             Capturara los datos de la tabla y los guardara en variables para posteriormente crear un objeto 
            (Ahora si la cantidad) y guardarlo en una lista que sera enviada al formulario de carrito y mostrarlo 
             */
            if (e.RowIndex >= 0 && e.ColumnIndex == tablaProductos.Columns["Accion"].Index)
            {
                int id = Convert.ToInt32(tablaProductos.Rows[e.RowIndex].Cells["Id"].Value);
                string nombre = tablaProductos.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                double precio = Convert.ToDouble(tablaProductos.Rows[e.RowIndex].Cells["Precio"].Value);
                string descripcion= tablaProductos.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();



                // Crear un objeto ItemCarrito
                Producto producto = new Producto(id, nombre,descripcion, precio);

                // Mostrar MessageBox para confirmar la adición al carrito
                DialogResult resultado = MessageBox.Show("¿Deseas agregar el producto al carrito?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    carrito.AgregarAlCarrito(producto);
                    
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Al darle click a la opcion que dice carrito de compras esta mostrata el nuevo formulario y ocultara este
        private void TsmicarritoDeCompras_Click(object sender, EventArgs e)
        {
            //Al crear el formulario se le mandara un metodo que devuelve la lista de los productos que han sido guardados en el carrito de compras
            //para enviarlos al otro formulario
            CarritoCompra frcarritoCompra = new CarritoCompra(EnviarProducto(carrito.Productos));
            Hide();
            frcarritoCompra.ShowDialog();
            Show();
        }

        

        private void TsmiSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Con este metodo enviamos la lista de los productos que se almacenaran al formulario de carrito de compras
        //que tendra como parametro la lista que queremos enviarle (lo haremos por medio del constructor)
        private List<Producto> EnviarProducto(List<Producto> pro)
        {
            return pro;
        }

        private void porNombreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //BuscarNombre buscarNombre = new BuscarNombre();
           // Hide();
           // buscarNombre.ShowDialog();
          //  Show();
        }

        public void TraerProductosFiltrados(Producto p)
        {
            carrito.AgregarAlCarrito(p);
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            //Capturamos el texto que esta el textbox y lo guardamos en la variable
            string nombre = txtBuscarNombre.Text;
            //Si el texto coincide con alguna parte del nombre de los productos la lista vendra con objetos 
            //y si la lista lleva objetos en la variable hayProductos se almacenara true y si no pos guardara false
            bool hayProductos = conexion.BuscarProductosNombre(nombre).Count > 0 ? true : false;

            //Si hayProductos es true
            //Primero vaciaremos el datagrid (le quitamos todos los elementos)
            //y luego cargamos el metodo CargarProductos con el argumento del nombre del textbox y mostrara los tabla con los productos que tienen concordancia
            if (hayProductos)
            {
                tablaProductos.DataSource = null;
                CargarProductos(nombre);
            }else
            {
                MessageBox.Show("No hay productos");
            }
        }

        //Esto ignorenlo lo queria hacer por menu pero era mas complicado
        /* private void porNombreToolStripMenuItem_Click(object sender, EventArgs e)
         {
             BuscarNombres buscarNombres = new BuscarNombres();
             buscarNombres.ShowDialog();
         }*/

        /*private void btnMostrar_Click(object sender, EventArgs e)
        {
            string nombreBuscar = txtBuscarNombre.Text;
           // tablaProductos.DataSource = null;
            CargarProductos(nombreBuscar);
        }*/
    }
}
