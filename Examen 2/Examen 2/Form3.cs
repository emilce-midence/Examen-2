using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Crear la cadena de conexión a la base de datos
            string connectionString = "server=localhost;database=tu_base_de_datos;uid=tu_usuario;password=tu_contraseña;";

            // Crear la consulta SQL para insertar el ticket
            string query = "INSERT INTO tickets (fecha, usuario, cliente, tipo_soporte, descripcion_solicitud, descripcion_respuesta, precio, impuesto, descuento, total) " +
                           "VALUES (@fecha, @usuario, @cliente, @tipo_soporte, @descripcion_solicitud, @descripcion_respuesta, @precio, @impuesto, @descuento, @total)";

            // Crear la conexión a la base de datos y el objeto de comando para ejecutar la consulta
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Establecer los valores de los parámetros de la consulta con los datos del formulario
                command.Parameters.AddWithValue("@fecha", dtpFecha.Value);
                command.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                command.Parameters.AddWithValue("@cliente", txtCliente.Text);
                command.Parameters.AddWithValue("@tipo_soporte", cboTipoSoporte.SelectedItem.ToString());
                command.Parameters.AddWithValue("@descripcion_solicitud", txtDescripcionSolicitud.Text);
                command.Parameters.AddWithValue("@descripcion_respuesta", txtDescripcionRespuesta.Text);
                command.Parameters.AddWithValue("@precio", nudPrecio.Value);
                command.Parameters.AddWithValue("@impuesto", nudImpuesto.Value);
                command.Parameters.AddWithValue("@descuento", descuento);
                command.Parameters.AddWithValue("@total", total);

                command.ExecuteNonQuery();

                connection.Close();

            }
        }
