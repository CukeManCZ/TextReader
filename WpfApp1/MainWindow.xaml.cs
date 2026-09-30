using System.Windows;
using WpfApp1.Services;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(TextDocumentService textDocumentService)
        {
            InitializeComponent();

            MainWindowViewModel vm = new MainWindowViewModel(textDocumentService);
            DataContext = vm;
        }

        private void svTextScroll_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.UpdateViewPort(e.NewSize.Width, e.NewSize.Height);
            }
        }
    }
}