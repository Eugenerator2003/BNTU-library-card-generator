using Bntu.Library.CardGenerator.Domain.Entities;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Repositories
{
    [TestClass]
    public class ReaderCardCvsRepositoryTests
    {
        private static string[] Lines =
            [
                "Штрих-код;Дата записи в библиотеку;Дата окончания обучения;Фамилия;Имя Отчество;Факультет;Примечания ( продолж.) (номер группы);Группа;Фото;Мусор",
                "1234;12.08.2025;12.08.2028;Джипегов;Василан;Какультет;123;Студенты ДО;1.jpg;Поле",
                "1235;12.08.2025;12.08.2028;Василанов;Джипег;Какультет;123;Студенты ДО;2.jpg;выфджвждфылв",
                "1236;12.08.2025;12.08.2028;Трулимеро;Труличина;Какультет;123;Студенты ДО;3.jpg;",
                "1237;1.09.2025;1.09.2029;Трипитропа;Тропатрипа;Какультет;101;Студенты ЗО;4.jpg;312",
                "1238;1.09.2025;1.09.2029;Тропатрипа;Трипитропа;Какультет;101;Студенты ЗО;5.jpg;жвжыф",
                "1239;1.09.2025;1.09.2029;Арбузов;Огурец;ТУГФ;999;Студенты ДО;6.jpg;Не читать",
            ];

        private static ReaderCard[] Cards =
            [
                new ReaderCard(new CardInfo(1234, 1234, new DateTime(2025, 8, 12), new DateTime(2028, 8, 12)),
                               new Student("Джипегов", "Василан", new Faculty("Какультет"), new StudyGroup("123", StudyGroup.DepartmentType.Fulltime)),
                               new Photo("1.jpg")),
                new ReaderCard(new CardInfo(1235, 1235, new DateTime(2025, 8, 12), new DateTime(2028, 8, 12)),
                               new Student("Василанов", "Джипег", new Faculty("Какультет"), new StudyGroup("123", StudyGroup.DepartmentType.Fulltime)),
                               new Photo("2.jpg")),
                new ReaderCard(new CardInfo(1236, 1236, new DateTime(2025, 8, 12), new DateTime(2028, 8, 12)),
                               new Student("Трулимеро", "Труличина", new Faculty("Какультет"), new StudyGroup("123", StudyGroup.DepartmentType.Fulltime)),
                               new Photo("3.jpg")),
                new ReaderCard(new CardInfo(1237, 1237, new DateTime(2025, 9, 1), new DateTime(2029, 9, 1)),
                               new Student("Трипитропа", "Тропатрипа", new Faculty("Какультет"), new StudyGroup("101", StudyGroup.DepartmentType.Correspondence)),
                               new Photo("4.jpg")),
                new ReaderCard(new CardInfo(1238, 1238, new DateTime(2025, 9, 1), new DateTime(2029, 9, 1)),
                               new Student("Тропатрипа", "Трипитропа", new Faculty("Какультет"), new StudyGroup("101", StudyGroup.DepartmentType.Correspondence)),
                               new Photo("5.jpg")),
                new ReaderCard(new CardInfo(1239, 1239, new DateTime(2025, 9, 1), new DateTime(2029, 9, 1)),
                               new Student("Арбузов", "Огурец", new Faculty("ТУГФ"), new StudyGroup("999", StudyGroup.DepartmentType.Fulltime)),
                               new Photo("6.jpg")),
            ];

        private static string filepath = "test.csv";
        private static string invalidfilePath = "invalid.csv";
        private static string incorrectField = "";

        private static ReaderCardCvsRepository GetRepository()
        {
            return new ReaderCardCvsRepository(filepath);
        }

        private static void WriteIncorrect(int row, int column)
        {
            StreamWriter writer = new StreamWriter(invalidfilePath);
            writer.WriteLine(Lines[0]);
            for (int i = 1; i < Lines.Length; i++)
            {
                if (i != row)
                {
                    writer.WriteLine(Lines[i]);
                }
                else
                {
                    var words = Lines[i].Split(";");
                    for (int j = 0; j < words.Length; j++)
                    {
                        if (j != column)
                        {
                            writer.Write(words[j]);
                        }
                        else
                        {
                            incorrectField = Lines[0].Split(';')[j];
                        }
                        writer.Write(';');
                    }
                    writer.Write('\n');
                }
            }
            writer.Dispose();
        }

        [TestInitialize]
        public void Initialize()
        {
            File.WriteAllLines(filepath, Lines);
        }

        [TestMethod]
        public void GetAllCardsTest()
        {
            var repo = GetRepository();
            var actual = repo.GetAllCards();
            Assert.AreEqual(Cards.Length, actual.Count);
            for (int i = 0; i < Cards.Length; i++)
            {
                Assert.AreEqual(Cards[i], actual[i]);
            }
        }

        [TestMethod]
        public void GetCardByPidTest()
        {
            var repo = GetRepository();
            var actual = repo.GetCardByPid(1234);
            Assert.AreEqual(Cards.First(a => a.PId == 1234), actual);
        }

        [TestMethod]
        public void GetCardByCodbarTest()
        {
            var repo = GetRepository();
            var actual = repo.GetCardByPid(1239);
            Assert.AreEqual(Cards.First(a => a.PId == 1239), actual);
        }

        [TestMethod]
        public void GetCardByGroupTest()
        {
            var repo = GetRepository();
            var actual = repo.GetCardsByGroup("101");
            var expected = Cards.Where(a => a.StudyGroup == "101").ToArray();
            Assert.AreEqual(expected.Length, actual.Count);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void GetCardIOException()
        {
            var repo = new ReaderCardCvsRepository("nofile");
            Assert.ThrowsException<IOException>(repo.GetAllCards);
        }

        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(3, 3)]
        [DataRow(4, 4)]
        [DataRow(5, 5)]
        public void GetCardInvalidDataEception(int row, int column)
        {
            WriteIncorrect(row, column);
            var repo = new ReaderCardCvsRepository(invalidfilePath);
            var exception = Assert.ThrowsException<InvalidDataException>(repo.GetAllCards);
            var expectedMessage = $"Ошибка чтения данных в строке {row}. Текущее поле: {incorrectField}";
            Assert.IsTrue(exception.Message.Contains(expectedMessage));
        }
    }
}
