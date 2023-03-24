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
    public partial class Form2 : Form
    {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=soportetecnico;Uid=root;Pwd=password;");
            MySqlCommand cmd;
            MySqlDataAdapter da;
            DataTable dt;

            public LoginForm()
            {
                InitializeComponent();
            }

            private void btnLogin_Click(object sender, EventArgs e)
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario AND contrasena = @contrasena";
                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@contrasena", txtContrasena.Text);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("Bienvenido " + txtUsuario.Text);
                        this.Hide();
                        MenuForm menuForm = new MenuForm();
                        menuForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario y/o contraseña incorrectos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public partial class MenuForm : Form
        {
            public MenuForm()
            {
                InitializeComponent();
            }

            private void btnTickets_Click(object sender, EventArgs e)
            {
                TicketsForm ticketsForm = new TicketsForm();
                ticketsForm.ShowDialog();
            }
        }

        public partial class TicketsForm : Form
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=soportetecnico;Uid=root;Pwd=password;");
            MySqlCommand cmd;
            MySqlDataAdapter da;
            DataTable dt;

            public TicketsForm()
            {
                InitializeComponent();
            }

            private void TicketsForm_Load(object sender, EventArgs e)
            {
                conn.Open();
                string query = "SELECT nombre FROM clientes";
                cmd = new MySqlCommand(query, conn);
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                cmbCliente.DisplayMember = "nombre";
                cmbCliente.ValueMember = "nombre";
                cmbCliente.DataSource = dt;
                conn.Close();
            }

            private void btnGenerarTicket_Click(object sender, EventArgs e)
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO tickets (fecha, usuario, cliente, tipo_soporte, descripcion_solicitud, descripcion_respuesta, precio, impuesto, descuento, total) VALUES (@fecha, @usuario, @cliente, @tipo_soporte, @descripcion_solicitud, @descripcion_respuesta, @precio, @impuesto, @descuento, @total)";
                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@usuario", Properties.Settings.Default.Usuario);
                    cmd.Parameters.AddWithValue("@cliente", cmbCliente.Text);
                    cmd.Parameters.AddWithValue("@tipo_soporte", cmbTipoSoporte.Text);
                    cmd.Parameters.AddWithValue("@descripcion_solicitud", txtDescripcionSolicitud.Text);
                    cmd.Parameters.AddWithValue("@descripcion_respuesta", txtDescripcionRespuesta.Text);
                    cmd.Parameters.AddWithValue("@precio", txtPrecio.Text);
                    cmd.Parameters.AddWithValue("@imp
    



    }
}
