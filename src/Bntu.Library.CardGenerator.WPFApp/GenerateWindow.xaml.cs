using Bntu.Library.CardGenerator.Application.UseCases;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Domain.Entities;
using Bntu.Library.CardGenerator.WPFApp.Factories;
using Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
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
using ZXing;

namespace Bntu.Library.CardGenerator.WPFApp
{
    /// <summary>
    /// Логика взаимодействия для GenerateWindow.xaml
    /// </summary>
    public partial class GenerateWindow : Window
    {
        private MainWindow mainWindow;
        private IServiceProvider services;

        private CsvFilesPathProvider csvFilesPathProvider = CsvFilesPathProvider.Instance;
        private HtmlFilesPathProvider htmlFilesPathProvider = HtmlFilesPathProvider.Instance;

        public GenerateWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainWindow.Visibility = Visibility.Hidden;
            services = ((App)System.Windows.Application.Current).Services;
            Cleanup();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }

        private void ChooseCsvFile(object sender, RoutedEventArgs e)
        {
            Cleanup();
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            var dialogResult = dialog.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                MessageBox.Show(this, "Не выбран файл", "Ошибка");
                PhotoGrid.IsEnabled = false;
                return;
            }
            else
            {
                csvFilesPathProvider.ReaderCardsSource = dialog.FileName;
                PhotoGrid.IsEnabled = true;
            }
        }

        private void ChoosePhotoFolder(object sender, RoutedEventArgs e)
        {
            CleanupFolder();
            var dialog = new OpenFolderDialog();
            var dialogResult = dialog.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                MessageBox.Show(this, "Не выбран папка", "Ошибка");
                FolderGrid.IsEnabled = false;
                return;
            }
            else
            {
                htmlFilesPathProvider.PhotoFolder = dialog.FolderName;
                FolderGrid.IsEnabled = true;
            }
        }

        private void ChooseRootFolder(object sender, RoutedEventArgs e)
        {
            CleanupSave();
            var dialog = new OpenFolderDialog();
            var dialogResult = dialog.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                MessageBox.Show(this, "Не выбран папка", "Ошибка");
                SaveGrid.IsEnabled = false;
                return;
            }
            else
            {
                htmlFilesPathProvider.RootFolder = dialog.FolderName;
                SaveGrid.IsEnabled = true;
            }
        }

        private void ExportReaderCards(object sender, RoutedEventArgs e)
        {
            var group = GroupBox.Text;
            if (string.IsNullOrEmpty(group))
            {
                MessageBox.Show(this, "Не указана группа", "Ошибка");
                return;
            }

            var getParameter = new GetReaderCardsCaseParameter([group]);
            var getUseCase = services.GetRequiredService<GetReaderCardsUseCase>();
            var getReaderCardsResult = getUseCase.GetReaderCards(getParameter);
            if (!getReaderCardsResult.Success)
            {
                MessageBox.Show(this, getReaderCardsResult.Message, "Ошибка");
                ReaderCardsMessage.Text = getReaderCardsResult.Message;
                return;
            }

            var generateParameter = getReaderCardsResult.ToGenerateReaderCardsCaseParameter();
            var generateUseCase = services.GetRequiredService<GenerateCardsUseCase>();
            var generateResult = generateUseCase.Generate(generateParameter);
            if (!generateResult.Success)
            {
                MessageBox.Show(this, generateResult.Message, "Ошибка");
                ReaderCardsMessage.Text = generateResult.Message;
                return;
            }

            var exportParameter = generateResult.ToExportReaderCardsCaseParameter(group);
            htmlFilesPathProvider.GroupFolder = group;
            var exportUseCase = services.GetRequiredService<ExportReaderCardsUseCase>();
            var exportResult = exportUseCase.Export(exportParameter);
            if (!exportResult.Success)
            {
                MessageBox.Show(this, exportResult.Message, "Ошибка");
                return;
            }
            ReaderCardsMessage.Text = exportResult.Message;
        }

        private void Cleanup()
        {
            ReaderCardsMessage.Text = "Н/д";
            CleanupPhoto();
        }

        private void CleanupPhoto()
        {
            PhotoGrid.IsEnabled = false;
            CleanupFolder();
        }

        private void CleanupFolder()
        {
            FolderGrid.IsEnabled = false;
            CleanupSave();
        }

        private void CleanupSave()
        {
            SaveGrid.IsEnabled = false;
        }

        private void OnlyDigits(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void OnlyDigitsPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!text.All(char.IsDigit))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
