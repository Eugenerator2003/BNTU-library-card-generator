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

namespace Bntu.Library.CardGenerator.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenImportWindow(object sender, RoutedEventArgs e)
        {
            ImportWindow importWindow = new ImportWindow(this);
            importWindow.Visibility = Visibility.Visible;
        }

        private void OpenGenerateWindow(object sender, RoutedEventArgs e)
        {
            GenerateWindow window = new GenerateWindow(this);
            window.Visibility = Visibility.Visible;
        }
    }
}