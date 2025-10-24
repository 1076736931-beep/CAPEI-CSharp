using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Segcapei
{
    public partial class VentanaGenerador : Window
    {
        private readonly List<string> _historial = new();
        private readonly string _rutaHistorial;
        private DispatcherTimer _temporizadorCopia;
        private DateTime _tiempoCopia;

        public VentanaGenerador()
        {
            InitializeComponent();
            _rutaHistorial = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Historial_SEGCAPEI.txt"
            );

            InicializarTemporizador();
            CargarHistorial();
        }

        #region 🔁 Inicialización del temporizador (borrado automático del portapapeles)
        private void InicializarTemporizador()
        {
            _temporizadorCopia = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _temporizadorCopia.Tick += (s, e) =>
            {
                if (DateTime.Now >= _tiempoCopia)
                {
                    Clipboard.Clear();
                    _temporizadorCopia.Stop();
                }
            };
        }
        #endregion

        #region ⚙️ Configuración del generador
        private void SliderLongitud_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtLongitud != null)
                TxtLongitud.Text = ((int)SliderLongitud.Value).ToString();
        }

        private string ObtenerCaracteres()
        {
            StringBuilder sb = new();

            if (ChkMayusculas.IsChecked == true)
                sb.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            if (ChkMinusculas.IsChecked == true)
                sb.Append("abcdefghijklmnopqrstuvwxyz");
            if (ChkNumeros.IsChecked == true)
                sb.Append("0123456789");
            if (ChkSimbolos.IsChecked == true)
                sb.Append("!@#$%^&*()-_=+[]{};:,.<>?");

            return sb.ToString();
        }
        #endregion

        #region 🔐 Generación de contraseñas
        private void BtnGenerar_Click(object sender, RoutedEventArgs e)
        {
            int longitud = (int)SliderLongitud.Value;
            int cantidad = int.Parse(((ComboBoxItem)CmbCantidad.SelectedItem).Content.ToString());
            string caracteres = ObtenerCaracteres();

            if (string.IsNullOrEmpty(caracteres))
            {
                MessageBox.Show("Seleccione al menos un tipo de carácter.",
                                "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            for (int i = 0; i < cantidad; i++)
            {
                string pass = GenerarPassword(longitud, caracteres);
                string entrada = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}  →  {pass}";
                _historial.Insert(0, entrada);
            }

            RefrescarLista();
            GuardarHistorial();
        }

        private string GenerarPassword(int longitud, string caracteres)
        {
            var sb = new StringBuilder(longitud);

            for (int i = 0; i < longitud; i++)
            {
                int index = RandomNumberGenerator.GetInt32(caracteres.Length);
                sb.Append(caracteres[index]);
            }

            return sb.ToString();
        }
        #endregion

        #region 📋 Copiar contraseñas con límite de tiempo
        private void BtnCopiar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string texto)
            {
                Clipboard.SetText(texto);
                _tiempoCopia = DateTime.Now.AddSeconds(10);
                _temporizadorCopia.Start();
            }
        }

        private void BtnCopiarTodas_Click(object sender, RoutedEventArgs e)
        {
            if (_historial.Count == 0)
            {
                MessageBox.Show("No hay contraseñas para copiar.",
                                "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Clipboard.SetText(string.Join(Environment.NewLine, _historial));
            _tiempoCopia = DateTime.Now.AddSeconds(10);
            _temporizadorCopia.Start();
        }
        #endregion

        #region 🧹 Limpiar y exportar historial
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Desea limpiar todo el historial?",
                                         "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _historial.Clear();
                RefrescarLista();
                GuardarHistorial();
            }
        }

        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            if (_historial.Count == 0)
            {
                MessageBox.Show("No hay contraseñas para exportar.",
                                "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"Export_SEGCAPEI_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            );

            File.WriteAllLines(ruta, _historial);
            MessageBox.Show($"Historial exportado correctamente:\n{ruta}",
                            "Exportado", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region 💾 Guardado automático del historial
        private void GuardarHistorial()
        {
            try
            {
                File.WriteAllLines(_rutaHistorial, _historial);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar historial: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarHistorial()
        {
            try
            {
                if (File.Exists(_rutaHistorial))
                {
                    _historial.Clear();
                    _historial.AddRange(File.ReadAllLines(_rutaHistorial));
                    RefrescarLista();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefrescarLista()
        {
            ListaPasswords.ItemsSource = null;
            ListaPasswords.ItemsSource = _historial;
            TxtEstadisticas.Text = $"Total generadas: {_historial.Count}";
        }
        #endregion
    }
}
