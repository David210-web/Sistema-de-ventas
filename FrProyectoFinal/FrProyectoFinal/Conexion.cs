using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrProyectoFinal
{
    internal class Conexion
    {
        //Creo la cadena de conexion 
        /*
         Data Source es para el nombre del servidor (cambiar al nombre de tu servidor)
        Initial Catalog es el nombre de la base de datos (cambiar al nombre que le has puesto a tu base de datos donde va
        la tabla)
         */
        private string cadenaConexion = "Data Source = DESKTOP-AO83001;Initial Catalog = DB_SISTEMA_VENTAS;Integrated Security = true";

        //El metodo retornara una lista de Productos
        public List<Producto> MostrarProductos()
        {
            List<Producto> productosFiltrados = new List<Producto>();
            string query = "SELECT ID_Producto,Nombre,Descripcion,Precio,Cantidad_Stock FROM Productos";

            //Llamamos a la clase SqlConexion y como parametro la pasamos la cadena de conexion
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                //El commmand sirve para hacer las consultas a la base
                SqlCommand cmd = new SqlCommand(query, conexion);
                try
                {
                    //Abrimos la conexion y cremos un reader para poder leer los datos de la tabla
                    conexion.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        //Creamos variables temporales para que almacenen los datos
                        int id = sqlDataReader.GetInt32(0);
                        string nombre = sqlDataReader.GetString(1);
                        string descripcion = sqlDataReader.GetString(2);
                        double precio = Convert.ToDouble(sqlDataReader.GetDecimal(3));
                        int cantidad = sqlDataReader.GetInt32(4);
                        //Ahora creamos un producto que le pasamos las variables anteriormente creadas en el constructor
                        Producto producto = new Producto(id,nombre,descripcion,precio,cantidad);
                        //Agregamos el producto a la lista
                        productosFiltrados.Add(producto);
                    } 
                    sqlDataReader.Close();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            //Y retornamos la lista
            return productosFiltrados;
        }

        //Este es un metodo que retorna igualmente una lista de productos con la diferencia que hoy buscamos las coinciedencias 
        public List<Producto> BuscarProductosNombre(string nombreProducto)
        {
            List<Producto> productos = new List<Producto>();

            string nombreProcedimiento = "SpBuscarProductoPorNombre";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // El command sirve para hacer las consultas a la base
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);

                // Agregamos el parámetro para el nombre del producto
                cmd.Parameters.AddWithValue("@nombre", nombreProducto);
                // Especificamos que es un procedimiento
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();

                    // Abrimos la conexión y creamos un reader para poder leer los datos de la tabla
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        // Creamos variables temporales para almacenar los datos
                        int id = sqlDataReader.GetInt32(0);
                        string nombre = sqlDataReader.GetString(1);
                        string descripcion = sqlDataReader.GetString(2);
                        double precio = Convert.ToDouble(sqlDataReader.GetDecimal(3));
                        int cantidad = sqlDataReader.GetInt32(4);

                        // Creamos un producto y lo agregamos a la lista
                        Producto producto = new Producto(id, nombre, descripcion, precio, cantidad);
                        productos.Add(producto);
                    }

                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return productos;
        }

        public List<Producto> BuscarProductosPrecio(double rangoInicial, double rangoFinal)
        {
            List<Producto> productosFiltrados = new List<Producto>();

            string nombreProcedimiento = "SpBuscarPreciosPorRango";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // El command sirve para hacer las consultas a la base
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);

                // Agregamos el parámetro para el nombre del producto
                cmd.Parameters.AddWithValue("@RangoInicial", rangoInicial);
                cmd.Parameters.AddWithValue("@RangoFinal", rangoFinal);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();

                    // Abrimos la conexión y creamos un reader para poder leer los datos de la tabla
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        // Creamos variables temporales para almacenar los datos
                        int id = sqlDataReader.GetInt32(0);
                        string nombre = sqlDataReader.GetString(1);
                        string descripcion = sqlDataReader.GetString(2);
                        double precio = Convert.ToDouble(sqlDataReader.GetDecimal(3));
                        int cantidad = sqlDataReader.GetInt32(4);

                        // Creamos un producto y lo agregamos a la lista
                        Producto producto = new Producto(id, nombre, descripcion, precio, cantidad);
                        productosFiltrados.Add(producto);
                    }

                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return productosFiltrados;
        }

        public bool InsertarCliente(string nombre, string direccion, string informacionContacto)
        {
            string query = "INSERT INTO Clientes (Nombre, Direccion, Informacion_Contacto) VALUES (@Nombre, @Direccion, @InformacionContacto)";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    try
                    {
                        conexion.Open();
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);
                        cmd.Parameters.AddWithValue("@InformacionContacto", informacionContacto);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        return filasAfectadas > 0; // Devuelve verdadero si se insertó al menos una fila
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false; // Si ocurre un error, devuelve falso
                    }
                }
            }
        }

        public bool GuardarPedidos(int idCliente,double doubleTotal)
        {
            string query = "INSERT INTO Pedidos (ID_Cliente,Total) VALUES (@idCliente,@doubleTotal)";
            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using(SqlCommand cmd = new SqlCommand(query,conexion))
                {
                    try
                    {
                        conexion.Open();
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Parameters.AddWithValue("@doubleTotal", doubleTotal);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        return filasAfectadas > 0; // Devuelve verdadero si se insertó al menos una fila
                    }catch(Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public int ObtenerIdCliente(string nombre)
        {
            string nombreProcedimiento = "SP_GetId";
            int id = 0;  // Inicializar la variable

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // El command sirve para hacer las consultas a la base
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);

                // Agregamos el parámetro para el nombre del producto
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();

                    // Abrimos la conexión y creamos un reader para poder leer los datos de la tabla
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        // Creamos variables temporales para almacenar los datos
                        id = sqlDataReader.GetInt32(0);
                    }

                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return id;  
        }


    }
}
