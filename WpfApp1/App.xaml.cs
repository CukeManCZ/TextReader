using System.Windows;
using WpfApp1.Services;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var textService = new TextDocumentService();
            var window = new MainWindow(textService);
            window.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }

}
