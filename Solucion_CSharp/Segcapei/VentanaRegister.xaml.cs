using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Segcapei
{
    public partial class VentanaRegister : Window
    {
        public VentanaRegister()
        {
            InitializeComponent();
        }

        // Agregar eventos para los botones si los tienes en el XAML

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        { 
            MainWindow ventanaLogin = new MainWindow();
            ventanaLogin.Show();
            this.Close();
        }
        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = TxtUsername.Text;
            string clave = TxtPassword.Password;
            string confirmarClave = TxtConfirmPassword.Password;

            //validar campos vacios
                if (string.IsNullOrWhiteSpace(usuario)  ||
                    string.IsNullOrWhiteSpace(clave) ||
                    string.IsNullOrWhiteSpace(confirmarClave))
                {
                    MessageBox.Show("Por favor, completar todos los campos");
                }

            //validar claves
                if (clave != confirmarClave) 
                {
                    MessageBox.Show("❌ Las contraseñas no coinciden");
                } 

            try
            {
                using var conn = ConexionDB.GetOpenConnection();
                string sql = "INSERT INTO usuarios (username, password) VALUES (@user, @pass);";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", usuario);
                cmd.Parameters.AddWithValue("@pass", clave);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Usuario registrado exitosamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario.");
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show("Error: " + ex.Message);
            }
        {
            MainWindow ventanaLogin = new MainWindow();
            ventanaLogin.Show();
            this.Close();
        }
        }

    }
}