using System.Configuration;
using System.Data;
using System.Windows;

namespace Segcapei
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 👇 aquí eliges qué ventana se abre primero
            MainWindow ventana = new MainWindow();
            ventana.Show();
        }

    }

}
