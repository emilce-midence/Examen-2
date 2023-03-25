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
    public partial class TicketForm : Form
    {
        public TicketForm()
        {
            InitializeComponent();
        }

        private void btnSubmitTicket_Click(object sender, EventArgs e)
        {
            // Obtener los datos del formulario de ticket
            DateTime fecha = DateTime.Now;
            string usuario = txtUsuario.Text;
            string cliente = txtCliente.Text;
            string tipoSoporte = cbTipoSoporte.Text;
            string descripcionSolicitud = txtDescripcionSolicitud.Text;
            string descripcionRespuesta = txtDescripcionRespuesta.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            decimal impuesto = decimal.Parse(txtImpuesto.Text);
            decimal descuento = decimal.Parse(txtDescuento.Text);
            decimal total = precio + impuesto - descuento;

            // Guardar los datos en la base de datos MySQL
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=mydatabase;Uid=myusername;Pwd=mypassword;");
            connection.Open();

            MySqlCommand command = new MySqlCommand("INSERT INTO tickets (fecha, usuario, cliente, tipo_soporte, descripcion_solicitud, descripcion_respuesta, precio, impuesto, descuento, total) VALUES (@fecha, @usuario, @cliente, @tipoSoporte, @descripcionSolicitud, @descripcionRespuesta, @precio, @impuesto, @descuento, @total)", connection);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.Parameters.AddWithValue("@cliente", cliente);
            command.Parameters.AddWithValue("@tipoSoporte", tipoSoporte);
            command.Parameters.AddWithValue("@descripcionSolicitud", descripcionSolicitud);
            command.Parameters.AddWithValue("@descripcionRespuesta", descripcionRespuesta);
            command.Parameters.AddWithValue("@precio", precio);
            command.Parameters.AddWithValue("@impuesto", impuesto);
            command.Parameters.AddWithValue("@descuento", descuento);
            command.Parameters.AddWithValue("@total", total);

            command.ExecuteNonQuery();

            connection.Close();

            // Mostrar un mensaje de confirmación
            MessageBox.Show("El ticket ha sido creado exitosamente.", "Ticket creado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Cerrar el formulario de ticket
            this.Close();
        }
    }


}