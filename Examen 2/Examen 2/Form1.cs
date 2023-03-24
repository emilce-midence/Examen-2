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
    public partial class Form1 : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Validar el inicio de sesión
            string usuario = txtUsuario.Text;
            string password = txtPassword.Text;
            if (usuario == "admin" && password == "123456")
            {
                Menu menu = new Menu();
                this.Hide();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos. Por favor intente de nuevo.");
            }
        }

    }

    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnTickets_Click(object sender, EventArgs e)
        {
            Tickets tickets = new Tickets();
            tickets.Show();
        }
    }

    public partial class Tickets : Form
    {
        public Tickets()
        {
            InitializeComponent();
        }

        private void btnGenerarTicket_Click(object sender, EventArgs e)
        {
            // Obtener los datos del formulario
            DateTime fecha = DateTime.Now;
            string usuario = "Admin";
            string cliente = txtCliente.Text;
            string tipoSoporte = cbTipoSoporte.SelectedItem.ToString();
            string descripcionSolicitud = txtDescripcionSolicitud.Text;
            string descripcionRespuesta = txtDescripcionRespuesta.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            decimal impuesto = decimal.Parse(txtImpuesto.Text);
            decimal descuento = decimal.Parse(txtDescuento.Text);
            decimal total = precio + impuesto - descuento;

            // Insertar el ticket en la base de datos
            string connectionString = "Data Source=SERVIDOR;Initial Catalog=BASE_DE_DATOS;User ID=USUARIO;Password=CONTRASEÑA";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO Tickets (Fecha, Usuario, Cliente, TipoSoporte, DescripcionSolicitud, DescripcionRespuesta, Precio, Impuesto, Descuento, Total) VALUES (@Fecha, @Usuario, @Cliente, @TipoSoporte, @DescripcionSolicitud, @DescripcionRespuesta, @Precio, @Impuesto, @Descuento, @Total)", connection);
            command.Parameters.AddWithValue("@Fecha", fecha);
            command.Parameters.AddWithValue("@Usuario", usuario);
            command.Parameters.AddWithValue("@Cliente", cliente);
            command.Parameters.AddWithValue("@TipoSoporte", tipoSoporte);
            command.Parameters.AddWithValue("@DescripcionSolicitud", descripcionSolicitud);
            command.Parameters.AddWithValue("@DescripcionRespuesta", descripcionRespuesta);
            command.Parameters.AddWithValue("@Precio", precio);
            command.Parameters.AddWithValue("@Impuesto", impuesto);
            command.Parameters.AddWithValue("@Descuento", descuento);
            command.Parameters.AddWithValue("@Total", total);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("El ticket ha sido generado exitosamente.");
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al generar el ticket. Por favor intente de nuevo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al generar el ticket. Por favor intente de nuevo." + Environment.NewLine + ex.Message);
            }
            finally
            {
                connection.Close();


            }
        }
    }
}
