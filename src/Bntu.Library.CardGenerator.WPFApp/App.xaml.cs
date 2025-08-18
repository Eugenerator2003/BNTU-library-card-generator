using Bntu.Library.CardGenerator.WPFApp.PathProviders;
using Bntu.Library.CardGenerator.WPFApp.Startup;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Bntu.Library.CardGenerator.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public IServiceCollection Services { get; }

        public CsvFilesPathProvider CsvPathProvider { get; }

        public HtmlFilesPathProvider HtmlPathProvider { get; }

        public XlsxFilesPathProvider XlsxPathProvider { get; }

        public App()
        {
            Services = new ServiceCollection().UseApplication()
                                              .UseStudentsImport()
                                              .UseTables()
                                              .UseHtmlExport();
            CsvPathProvider = CsvFilesPathProvider.Instance;
            HtmlPathProvider = HtmlFilesPathProvider.Instance;
            XlsxPathProvider = XlsxFilesPathProvider.Instance;
        }
    }
}
