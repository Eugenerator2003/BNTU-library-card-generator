using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport
{
    public class HtmlGeneratedCardExporter : IGeneratedCardExporter
    {
        private static string GroupPhotosFolder = "photos";
        private static string GroupBarcodesFolder = "barcodes";

        private string rootFolder;
        private string? subfolder;
        private string groupFolder;
        private string photoFolder;
        private string workDirectory = null!;
        private TemplatesPathContext templatePathContext;
        private BarcodeGenerator barcodeGenerator;
        private TemplatesContext templatesContext = null!;

        public HtmlGeneratedCardExporter(ExportFoldersContext foldersContext, TemplatesPathContext templatesPath)
        {
            rootFolder = foldersContext.RootFolder;
            subfolder = foldersContext.Subfolder;
            groupFolder = foldersContext.GroupFolder;
            photoFolder = foldersContext.PhotoFolder;
            templatePathContext = templatesPath;
            barcodeGenerator = new BarcodeGenerator();
        }

        public ApplicationResult<EmptyResult> Export(IList<GeneratedReaderCard> values)
        {
            if (templatesContext is null)
            {
                LoadTemplates();
            }
            DirectoryPreparing();
            var document = ImportToDocument(values);

            throw new NotImplementedException();
        }

        private void DirectoryPreparing()
        {
            if (!Directory.Exists(rootFolder))
            {
                throw new DirectoryNotFoundException($"Корневая папка экспортирования {rootFolder} не существует.");
            }
            string path;
            if (!string.IsNullOrEmpty(subfolder))
            {
                path = Path.Combine(rootFolder, subfolder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(rootFolder);
                }
            }
            else
            {
                path = rootFolder;
            }

            path = Path.Combine(path, groupFolder);
            if (Directory.Exists(path) && Directory.EnumerateFiles(path).Any())
            {
                throw new DirectoryNotFoundException($"Папка для экспорта группы {groupFolder} уже существует и не пустая.");
            }
            else
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(GroupPhotosFolder);
                Directory.CreateDirectory(GroupBarcodesFolder);
            }

            if (!Directory.Exists(photoFolder))
            {
                throw new DirectoryNotFoundException($"Папка с фотографиями студентов не существует.");
            }

            workDirectory = path;
        }

        private void LoadTemplates()
        {
            if (!File.Exists(templatePathContext.DocumentPath))
            {
                throw new FileNotFoundException($"Шаблон документа {templatePathContext.DocumentPath} не существует.");
            }

            if (!File.Exists(templatePathContext.PaperPath))
            {
                throw new FileNotFoundException($"Шаблон страницы документа {templatePathContext.PaperPath} не существует.");
            }

            if (!File.Exists(templatePathContext.CardPath))
            {
                throw new FileNotFoundException($"Шаблон читательского билета {templatePathContext.CardPath} не существует.");
            }

            var document = File.ReadAllText(templatePathContext.DocumentPath);
            var paper = File.ReadAllText(templatePathContext.PaperPath);
            var card = File.ReadAllText(templatePathContext.CardPath);
            templatesContext = new TemplatesContext(document, paper, card);
        }

        private StringBuilder ImportToDocument(IList<GeneratedReaderCard> cards)
        {
            var doc = new StringBuilder(templatesContext.Document);
            var papers = new StringBuilder();
            var htmlCards = new StringBuilder();
            for(int i = 0; i < cards.Count; i++)
            {
                if (i % 10 == 0)
                {
                    papers.Append(new StringBuilder(templatesContext.Paper).Replace("{cards}", htmlCards.ToString()));
                    htmlCards.Clear();
                }
                var barcodeFileName = PrepareCardFiles(cards[i]);
                var exportedCard = GetExportedCard(cards[i], barcodeFileName);
                htmlCards.Append(exportedCard);
            }
            papers.Append(new StringBuilder(templatesContext.Paper).Replace("{cards}", htmlCards.ToString()));
            doc.Replace("{papers}", papers.ToString());
            return doc;
        }

        private string PrepareCardFiles(GeneratedReaderCard card)
        {
            var filePath = Path.Combine(photoFolder, card.PhotoName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Фотография студента {card.CodbarValue} с именем {card.PhotoName} не найдена в папке {photoFolder}.");
            }
            File.Copy(filePath, Path.Combine(workDirectory, GroupPhotosFolder, card.PhotoName));

            var codbarFileName = $"{card.CodbarValue}.png";
            filePath = Path.Combine(workDirectory, GroupBarcodesFolder, $"{codbarFileName}");
            barcodeGenerator.Generate(card.CodbarValue.ToString(), filePath);
            return codbarFileName;
        }

        private StringBuilder GetExportedCard(GeneratedReaderCard card, string barcodeFileName)
        {
            var exportedCard = new StringBuilder(templatesContext.Card);
            exportedCard.Replace("{codbar}", card.CodbarValue.ToString());
            exportedCard.Replace("{lastname}", card.Lastname);
            exportedCard.Replace("{givenName}", card.Firstname);
            exportedCard.Replace("{faculty}", card.Faculty);
            exportedCard.Replace("{department}", card.Department == Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime ? "Студент ДО" : "Студент ЗО");
            exportedCard.Replace("{registrationDate}", card.IssueDate.ToString("yyyy-MM-dd"));
            exportedCard.Replace("{graduationDate}", card.IssueDate.ToString("yyyy-MM-dd"));
            exportedCard.Replace("{portraitPath}", card.PhotoName);
            exportedCard.Replace("{barcodePath}", barcodeFileName);
            return exportedCard;
        }
    }
}
