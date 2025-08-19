using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
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

namespace Bntu.Library.CardGenerator.WPFApp
{
    /// <summary>
    /// Логика взаимодействия для ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        private MainWindow mainWindow;
        private IServiceProvider services;
        private CsvFilesPathProvider csvFilesPathProvider = CsvFilesPathProvider.Instance;
        private XlsxFilesPathProvider xlsFilesPathProvider = XlsxFilesPathProvider.Instance;

        private IList<ExportedInformation> exportedInformation = null!;
        private IList<ImportInformation> importInformation = null!;

        public ImportWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainWindow.Visibility = Visibility.Hidden;
            services = ((App)System.Windows.Application.Current).Services;
            IssueYearBox.Text = DateTime.Now.Year.ToString();
            BarcodeBox.Text = "1234";
            Cleanup();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }

        private void LoadProfessions(object sender, RoutedEventArgs e)
        {
            Cleanup();
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            var dialogResult = dialog.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                MessageBox.Show(this, "Не выбран файл", "Ошибка");
                return;
            }
            csvFilesPathProvider.ProfessionSource = dialog.FileName;
            var useCase = services.GetRequiredService<GetProfessionsUseCase>();
            var result = useCase.GetProfessions();
            if (!result.Success)
            {
                MessageBox.Show(this, result.Message, "Ошибка");
            }
            else
            {
                StudyProvider.Instance.Professions = result.Professions;
                SpecializationGrid.IsEnabled = true;
            }
            ProfessionMessage.Text = result.Message;
        }

        private void LoadSpecializations(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            var dialogResult = dialog.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                MessageBox.Show(this, "Не выбран файл", "Ошибка");
                return;
            }
            csvFilesPathProvider.SpecializationSource = dialog.FileName;
            var useCase = services.GetRequiredService<GetSpecializationsUseCase>();
            var result = useCase.GetSpecializations();
            if (!result.Success)
            {
                CleanupExportedInformation();
                MessageBox.Show(this, result.Message, "Ошибка");
            }
            else
            {
                StudyProvider.Instance.Specialization = result.Specializations;
                XlsxGrid.IsEnabled = true;
            }
            SpecializationMessage.Text = result.Message;
        }

        private void LoadExportedInformation(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            var dialogResult = dialog.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                MessageBox.Show(this, "Не выбран файл", "Ошибка");
                return;
            }
            xlsFilesPathProvider.ExportedInformationSource = dialog.FileName;
            var useCase = services.GetRequiredService<GetExportedInformationUseCase>();
            var result = useCase.GetExportedInformation();
            if (!result.Success)
            {
                CleanupTransformation();
                MessageBox.Show(this, result.Message, "Ошибка");
            }
            else
            {
                exportedInformation = result.ExportedInformation;
                TransformationGrid.IsEnabled = true;
            }
            ExportedInformationMessage.Text = result.Message;
        }

        private void TranformExportedInformation(object sender, RoutedEventArgs e)
        {
            int year = int.Parse(IssueYearBox.Text);
            int barcode = int.Parse(BarcodeBox.Text);
            var importingParameter = new ImportingParameter(new DateTime(year, 1, 1), barcode);
            var useCase = services.GetRequiredService<TransformExportedInformationUseCase>();
            var caseParameter = new TransformExportedInformationCaseParameter(exportedInformation, importingParameter);
            var result = useCase.Transform(caseParameter);
            if (!result.Success)
            {
                MessageBox.Show(this, result.Message, "Ошибка");
                SaveGrid.IsEnabled = false;
            }
            else
            {
                importInformation = result.ImportInformation;
                SaveGrid.IsEnabled = true;
            }
            TransformationInformationMessage.Text = result.Message;
        }

        private void SaveTransformedInformation(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Title = "Сохранить файл",
                Filter = "CSV-файлы (*.csv)|*.csv|Все файлы (*.*)|*.*",
                DefaultExt = "csv",
                FileName = "import_students"
            };
            var dialogResult = dialog.ShowDialog();
            if (!dialogResult.HasValue || !dialogResult.Value)
            {
                MessageBox.Show(this, "Не выбран файл", "Ошибка");
                return;
            }
            csvFilesPathProvider.ImportInformationDestination = dialog.FileName;
            var useCase = services.GetRequiredService<SaveImportInformationUseCase>();
            var parameter = new SaveImportInformationCaseParameter(importInformation);
            var result = useCase.Save(parameter);
            if (!result.Success)
            {
                MessageBox.Show(this, result.Message, "Ошибка");
            }
            else
            {
                MessageBox.Show(this, "Данные для импорта были успешно сохранены.", "Успех");
            }
        }

        private void Cleanup()
        {
            CleanupProfesssions();
        }

        private void CleanupProfesssions()
        {
            ProfessionMessage.Text = "Н/д";
            CleanupSpecializations();
        }

        private void CleanupSpecializations()
        {
            SpecializationMessage.Text = "Н/д";
            SpecializationGrid.IsEnabled = false;
            CleanupExportedInformation();
        }

        private void CleanupExportedInformation()
        {
            ExportedInformationMessage.Text = "Н/д";
            XlsxGrid.IsEnabled = false;
            CleanupTransformation();
        }

        private void CleanupTransformation()
        {
            TransformationInformationMessage.Text = "Н/д";
            TransformationGrid.IsEnabled = false;
            SaveGrid.IsEnabled = false;
        }

        private void OnlyDigits(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void OnlyDigitsPasting(object sender, TextCompositionEventArgs e)
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
