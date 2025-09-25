using MySqlConnector;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Segcapei
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Evento para el botón Login
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = TxtUsername.Text;
            string clave = TxtPassword.Password; 

            try
            {
                using var conn = ConexionDB.GetOpenConnection();
                string sql = "SELECT COUNT(*) FROM usuarios WHERE username=@user AND password=@pass";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", usuario);
                cmd.Parameters.AddWithValue("@pass", clave);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("✅ Bienvenido " + usuario);
                    var ventana = new VentanaP();
                    ventana.Show();
                    this.Close();
                }

                else
                {
                    MessageBox.Show("❌ Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error: " + ex.Message);
            }
        }


        // Evento para el botón Register
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegister VentanaRegister = new VentanaRegister();
            VentanaRegister.Show();
            this.Close(); 
        }
    }
}