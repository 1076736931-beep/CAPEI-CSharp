using System;
using System.Windows;

namespace Segcapei
{
    public partial class VentanaP : Window
    {
        public VentanaP()
        {
            InitializeComponent();
        }

        #region Eventos de Navegación

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro de que desea cerrar sesión?",
                                            "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Módulo 'Dashboard' en desarrollo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Módulo 'Usuarios' en desarrollo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnGeneradorPasswords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ventanaGenerador = new VentanaGenerador
                {
                    Owner = this
                };
                ventanaGenerador.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el generador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Módulo 'Reportes' en desarrollo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Módulo 'Configuración' en desarrollo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
    }
}
