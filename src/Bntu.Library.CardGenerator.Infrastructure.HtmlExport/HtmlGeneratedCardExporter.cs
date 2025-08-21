using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.Util;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                if (templatesContext is null)
                {
                    LoadTemplates();
                }
                DirectoryPreparing();
                var document = ImportToDocument(values);
                File.WriteAllText(Path.Combine(workDirectory, $"{groupFolder}.html"), document.ToString(), Encoding.UTF8);
                var listDocument = GetStudentsList(values);
                File.WriteAllText(Path.Combine(workDirectory, $"{groupFolder}_студенты.html"), listDocument.ToString(), Encoding.UTF8);
                return ApplicationResult<EmptyResult>.Success(null, "Читательские билеты были успешно экспортированы в формат HTML.");
            }
            catch (Exception ex)
            {
                return ApplicationResult<EmptyResult>.Error($"Ошибка экспортирования: {ex.Message}");
            }
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
                var barcodeFileName = PrepareCardFiles(cards[i]);
                var exportedCard = GetExportedCard(cards[i], barcodeFileName);
                htmlCards.Append(exportedCard);
                if ((i + 1) % 10 == 0)
                {
                    papers.Append(new StringBuilder(templatesContext.Paper).Replace("{cards}", htmlCards.ToString()));
                    htmlCards.Clear();
                }
            }
            if (cards.Count % 10 != 0)
                papers.Append(new StringBuilder(templatesContext.Paper).Replace("{cards}", htmlCards.ToString()));
            doc.Replace("{papers}", papers.ToString());
            doc.Replace("{title}", $"Билеты группы {groupFolder}");
            return doc;
        }

        private string PrepareCardFiles(GeneratedReaderCard card)
        {
            var filePath = Path.Combine(photoFolder, card.PhotoName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Фотография студента {card.Lastname} {card.Firstname} с именем {card.PhotoName} не найдена в папке {photoFolder}.");
            }
            var groupPhotosFolder = Path.Combine(workDirectory, GroupPhotosFolder);
            if (!Directory.Exists(groupPhotosFolder))
            {
                Directory.CreateDirectory(groupPhotosFolder);
            }
            File.Copy(filePath, Path.Combine(groupPhotosFolder, card.PhotoName), true);

            var groupCodbarsFolder = Path.Combine(workDirectory, GroupBarcodesFolder);
            if (!Directory.Exists(groupCodbarsFolder))
            {
                Directory.CreateDirectory(groupCodbarsFolder);
            }

            var codbarFileName = $"{card.CodbarValue}.png";
            filePath = Path.Combine(groupCodbarsFolder, $"{codbarFileName}");
            barcodeGenerator.Generate(card.CodbarValue.ToString(), filePath);
            return codbarFileName;
        }

        private StringBuilder GetExportedCard(GeneratedReaderCard card, string barcodeFileName)
        {
            var exportedCard = new StringBuilder(templatesContext.Card);
            exportedCard.Replace("{codbar}", card.CodbarValue.ToString());
            exportedCard.Replace("{lastname}", card.Lastname);
            exportedCard.Replace("{givenNames}", card.Firstname);
            exportedCard.Replace("{faculty}", card.Faculty);
            exportedCard.Replace("{group}", card.StudyGroup);
            exportedCard.Replace("{department}", card.Department == Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime ? "Студент ДО" : "Студент ЗО");
            exportedCard.Replace("{registrationDate}", card.IssueDate.ToString("yyyy-MM-dd"));
            exportedCard.Replace("{graduationDate}", card.ExpirationDate.ToString("yyyy-MM-dd"));
            exportedCard.Replace("{portraitPath}", card.PhotoName);
            exportedCard.Replace("{barcodePath}", barcodeFileName);
            return exportedCard;
        }

        private StringBuilder GetStudentsList(IList<GeneratedReaderCard> cards)
        {
            StringBuilder list = new StringBuilder();
            var groupedCards = cards.GroupBy(a => new { a.Faculty, a.StudyGroup })
                                    .OrderBy(a => a.Key.Faculty)
                                    .ThenBy(a => a.Key.StudyGroup);
            var lastFaculty = "";
            var lastGroup = "";
            int index = 1;

            list.AppendLine("");
            foreach(var group in groupedCards)
            {
                if (lastFaculty != group.Key.Faculty)
                {
                    lastFaculty = group.Key.Faculty;
                    list.AppendLine($"<h1>Факультет {group.Key.Faculty}</h1>");
                }

                if (lastGroup != group.Key.StudyGroup)
                {
                    lastGroup = group.Key.StudyGroup;
                    list.AppendLine($"<h2>Группа {group.Key.StudyGroup}:</h2>");
                    index = 1;
                }

                foreach(var card in group)
                {
                    list.Append($"<h3>{index}. {card.Lastname} {card.Firstname}</h3>");
                    index++;
                }
            }
            list.AppendLine("</div>");
            StringBuilder document = new StringBuilder(templatesContext.Document);
            document.Replace("{papers}", list.ToString());
            document.Replace("{title}", $"Список группы {groupFolder}");
            return document;
        }
    }
}
