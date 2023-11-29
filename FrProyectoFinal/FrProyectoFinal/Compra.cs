using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrProyectoFinal
{
    public partial class Compra : Form
    {
        private double totalProductos;
        public Compra(double totalProductos)
        {
            InitializeComponent();
            this.totalProductos = totalProductos;
        }

        private void Compra_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label5.Text = textBox1.Text;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label6.Text = textBox2.Text;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label8.Text = textBox3.Text;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        Carrito car = new Carrito();
        private Conexion conexion = new Conexion();

       // private string cadenaConexion = "Data Source=DESKTOP-NDE8133;Initial Catalog=DB_SISTEMA_VENTAS;Integrated Security=true";

        

        private void button1_Click(object sender, EventArgs e)
        {
                if(ValidarInputs())
                {
                    string nombre = textBox1.Text;
                    string direccion = textBox2.Text;
                    string informacionContacto = textBox3.Text;

                    string mensaje = $"Nombre: {nombre}\n\nDirección: {direccion}\n\nMail: {informacionContacto}\n\nTotal a Pagar: ${totalProductos}\n\nFecha: {DateTime.Now}";

                    MessageBox.Show(mensaje, "Información del Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Intenta insertar el cliente en la base de datos
                    bool insercionExitosa = conexion.InsertarCliente(nombre, direccion, informacionContacto);
                    bool PedidoExistoso = conexion.GuardarPedidos(conexion.ObtenerIdCliente(nombre),totalProductos);
                    if (insercionExitosa && PedidoExistoso)
                        MessageBox.Show("Cliente insertado correctamente en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Hubo un problema al insertar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private bool ValidarInputs()
        {
            if(textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                MessageBox.Show("Agregue todos los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }




    }
}

