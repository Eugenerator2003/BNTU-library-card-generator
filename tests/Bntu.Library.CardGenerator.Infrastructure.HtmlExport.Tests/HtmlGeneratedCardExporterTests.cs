using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests.Barcodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests
{
    [TestClass]
    public class HtmlGeneratedCardExporterTests
    {
        private static List<GeneratedReaderCard> cards = null!;

        [TestInitialize]
        public void Initialize()
        {
            if (Directory.Exists("groups"))
                Directory.Delete("groups", true);
            Directory.CreateDirectory("groups");
            var issue = DateTime.Now;
            var graduation = issue.AddYears(5);
            cards = [
                new GeneratedReaderCard(1, 1, issue, graduation, "Валерьев", "Валерий Валерьянкович",
                                        "МИДО", "3123313", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, "portrait1.png"),
                new GeneratedReaderCard(2, 2, issue, graduation, "Кишков", "Прикол Смешновкович",
                                        "МИДО", "3213", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, "portrait2.png"),
                new GeneratedReaderCard(3, 3, issue, graduation, "Григорьев", "Георгий",
                                        "МИДО", "12397790", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, "portrait3.png"),
                new GeneratedReaderCard(4, 4, issue, graduation, "Тестовый", "Пример",
                                        "МИДО", "12397790", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, "portrait4.png"),
            ];
        }

        [TestMethod]
        public void ExportTest()
        {
            var folderContext = new ExportFoldersContext("groups", null, "test", "photo");
            var templatesPath = new TemplatesPathContext("templates/document.html", "templates/paper.html", "templates/card.html");

            var exporter = new HtmlGeneratedCardExporter(folderContext, templatesPath);
            var result = exporter.Export(cards);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(Directory.Exists("groups/test/photos"));
            var photoFiles = Directory.EnumerateFiles("groups/test/photos").ToArray();
            Assert.IsTrue(photoFiles.Length == 4);
            for (int i = 0; i < photoFiles.Length; i++)
            {
                var expected = File.ReadAllBytes($"photo/{cards[i].PhotoName}");
                var actual = File.ReadAllBytes($"groups/test/photos/{cards[i].PhotoName}");
                CollectionAssert.AreEqual(expected, actual);
            }
            Assert.IsTrue(Directory.Exists("groups/test/barcodes"));
            var barcodesFiles = Directory.EnumerateFiles("groups/test/barcodes").ToArray();
            Assert.IsTrue(barcodesFiles.Length == 4);
            var barcodeReader = new BarcodeTestReader();
            for (int i = 0; i < barcodesFiles.Length; i++)
            {
                var value = barcodeReader.Read(barcodesFiles[i]);
                Assert.AreEqual($"{i + 1}", value);
            }


            var expectedDocument = new StringBuilder(File.ReadAllText(templatesPath.DocumentPath));
            var paper = File.ReadAllText(templatesPath.PaperPath);
            var card = File.ReadAllText(templatesPath.CardPath);

            var papers = new StringBuilder();
            var expectedCards = new StringBuilder();
            for (int i = 0; i < cards.Count; i++)
            {
                var exportedCard = new StringBuilder(card);
                exportedCard.Replace("{codbar}", cards[i].CodbarValue.ToString());
                exportedCard.Replace("{lastname}", cards[i].Lastname);
                exportedCard.Replace("{givenNames}", cards[i].Firstname);
                exportedCard.Replace("{faculty}", cards[i].Faculty);
                exportedCard.Replace("{group}", cards[i].StudyGroup);
                exportedCard.Replace("{department}", cards[i].Department == Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime ? "Студент ДО" : "Студент ЗО");
                exportedCard.Replace("{registrationDate}", cards[i].IssueDate.ToString("yyyy-MM-dd"));
                exportedCard.Replace("{graduationDate}", cards[i].ExpirationDate.ToString("yyyy-MM-dd"));
                exportedCard.Replace("{portraitPath}", cards[i].PhotoName);
                exportedCard.Replace("{barcodePath}", $"{cards[i].CodbarValue}.png");
                expectedCards.Append(exportedCard);
                if ((i + 1) % 10 == 0)
                {
                    papers.Append(new StringBuilder(paper).Append(expectedCards.ToString()));
                    expectedCards.Clear();
                }
            }
            if (cards.Count % 10 != 0)
                papers.Append(new StringBuilder(paper).Replace("{cards}", expectedCards.ToString()));
            expectedDocument.Replace("{papers}", papers.ToString());
            expectedDocument.Replace("{title}", $"Билеты группы test");

            var actualDocument = File.ReadAllText("groups/test/test.html");
            Assert.AreEqual(expectedDocument.ToString(), actualDocument);
        }
    }
}
