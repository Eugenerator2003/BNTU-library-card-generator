using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Services.Csv
{
    [TestClass]
    public class ImportInformationCsvSaverTests
    {
        private static ImportInformation[] infos = [
            new ImportInformation(1, "photo1.jpg",
                new ImportStudentInformation(Person.SexType.Male, "Петров", "Петр Петрович", new DateTime(2003, 1, 21), "HB330123 РОВД г. Гомеля"),
                new ImportContactInformation("г. Гомель", "пр-т. Речицкий, д.120, кв. 21", ["375444891602"], "petrov@gmail.com"),
                new ImportStudyGroup(new Faculty("ФИТР-25"), new Profession(1, "Профессионал 1-330-2"),
                    new Specialization(1, "Специалист 1-330-2-13", 1), "123",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, new DateTime(2030, 9, 1)),
                new LibraryRegistration(new DateTime(2025, 9, 1))
            ),
            new ImportInformation(2, "photo2.jpg",
                new ImportStudentInformation(Person.SexType.Female, "Иванова", "Анна Сергеевна", new DateTime(2002, 5, 14), "MP221456 РОВД г. Минска"),
                new ImportContactInformation("г. Минск", "ул. Ленина, д.5, кв. 14", ["375297654321"], "ivanova.a@mail.ru"),
                new ImportStudyGroup(new Faculty("ФИТР-21"), new Profession(2, "Инженер 1-370-1"),
                    new Specialization(2, "Программист 1-370-1-07", 2), "321",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, new DateTime(2029, 6, 30)),
                new LibraryRegistration(new DateTime(2025, 9, 1))
            ),
            new ImportInformation(3, "photo3.jpg",
                new ImportStudentInformation(Person.SexType.Male, "Сидоров", "Иван Ильич", new DateTime(2001, 12, 3), "KH789654 РОВД г. Бреста"),
                new ImportContactInformation("г. Брест", "ул. Советская, д.50", ["375331234567"], "sidorov@tut.by"),
                new ImportStudyGroup(new Faculty("ФИТР-30"), new Profession(3, "Архитектор 1-520-4"),
                    new Specialization(3, "Дизайнер 1-520-4-05", 3), "456",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Correspondence, new DateTime(2028, 5, 15)),
                new LibraryRegistration(new DateTime(2025, 9, 2))
            ),
            new ImportInformation(4, "photo4.jpg",
                new ImportStudentInformation(Person.SexType.Female, "Кузнецова", "Мария Павловна", new DateTime(2004, 7, 19), "AB456789 РОВД г. Витебска"),
                new ImportContactInformation("г. Витебск", "пр-т. Московский, д.75, кв. 8", ["375296789012"], "m.kuznetsova@gmail.com"),
                new ImportStudyGroup(new Faculty("ФИТР-15"), new Profession(4, "Экономист 1-250-2"),
                    new Specialization(4, "Бухгалтер 1-250-2-04", 4), "654",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, new DateTime(2031, 1, 20)),
                new LibraryRegistration(new DateTime(2025, 9, 2))
            ),
            new ImportInformation(5, "photo5.jpg",
                new ImportStudentInformation(Person.SexType.Male, "Васильев", "Дмитрий Николаевич", new DateTime(2002, 9, 10), "BM112233 РОВД г. Гродно"),
                new ImportContactInformation("г. Гродно", "ул. Победы, д.3", ["375445678901"], "d.vasiliev@mail.ru"),
                new ImportStudyGroup(new Faculty("ФИТР-12"), new Profession(5, "Строитель 1-450-3"),
                    new Specialization(5, "Проектировщик 1-450-3-02", 5), "789",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Correspondence, new DateTime(2027, 12, 10)),
                new LibraryRegistration(new DateTime(2025, 9, 3))
            ),
            new ImportInformation(6, "photo6.jpg",
                new ImportStudentInformation(Person.SexType.Female, "Егорова", "Светлана Викторовна", new DateTime(2003, 11, 25), "MN334455 РОВД г. Могилева"),
                new ImportContactInformation("г. Могилев", "ул. Первомайская, д.9, кв. 15", ["375299876543"], "s.egorova@mail.ru"),
                new ImportStudyGroup(new Faculty("ФИТР-18"), new Profession(6, "Химик 1-600-1"),
                    new Specialization(6, "Технолог 1-600-1-09", 6), "147",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, new DateTime(2029, 2, 28)),
                new LibraryRegistration(new DateTime(2025, 9, 4))
            ),
            new ImportInformation(7, "photo7.jpg",
                new ImportStudentInformation(Person.SexType.Male, "Фёдоров", "Алексей Андреевич", new DateTime(2001, 3, 2), "PP998877 РОВД г. Минска"),
                new ImportContactInformation("г. Минск", "ул. Кропоткина, д.14", ["375295432198"], "a.fedorov@mail.ru"),
                new ImportStudyGroup(new Faculty("ФИТР-22"), new Profession(7, "Механик 1-370-5"),
                    new Specialization(7, "Инженер-механик 1-370-5-08", 7), "258",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Correspondence, new DateTime(2028, 4, 1)),
                new LibraryRegistration(new DateTime(2025, 9, 5))
            ),
            new ImportInformation(8, "photo8.jpg",
                new ImportStudentInformation(Person.SexType.Female, "Григорьева", "Ольга Петровна", new DateTime(2004, 4, 18), "LK554433 РОВД г. Гомеля"),
                new ImportContactInformation("г. Гомель", "ул. Советская, д.11", ["375291112233"], "o.grigorieva@mail.ru"),
                new ImportStudyGroup(new Faculty("ФИТР-19"), new Profession(8, "Физик 1-510-1"),
                    new Specialization(8, "Лаборант 1-510-1-06", 8), "369",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, new DateTime(2030, 11, 15)),
                new LibraryRegistration(new DateTime(2025, 9, 6))
            ),
            new ImportInformation(9, "photo9.jpg",
                new ImportStudentInformation(Person.SexType.Male, "Андреев", "Виктор Степанович", new DateTime(2002, 8, 9), "RT667788 РОВД г. Витебска"),
                new ImportContactInformation("г. Витебск", "ул. Лесная, д.20", ["375296661122"], "v.andreev@mail.ru"),
                new ImportStudyGroup(new Faculty("ФИТР-16"), new Profession(9, "Биолог 1-640-2"),
                    new Specialization(9, "Исследователь 1-640-2-10", 9), "741",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Correspondence, new DateTime(2029, 8, 20)),
                new LibraryRegistration(new DateTime(2025, 9, 7))
            ),
            new ImportInformation(10, "photo10.jpg",
                new ImportStudentInformation(Person.SexType.Female, "Ковалёва", "Екатерина Олеговна", new DateTime(2003, 6, 30), "ZX889900 РОВД г. Бреста"),
                new ImportContactInformation("г. Брест", "ул. Центральная, д.7, кв. 3", ["375333210987"], "e.kovaleva@mail.ru"),
                new ImportStudyGroup(new Faculty("ФИТР-17"), new Profession(10, "Юрист 1-730-1"),
                    new Specialization(10, "Адвокат 1-730-1-11", 10), "852",
                    Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime, new DateTime(2031, 5, 5)),
                new LibraryRegistration(new DateTime(2025, 9, 8))
            )
        ];

        private static string testFile = "actual.csv";

        [TestCleanup]
        public void Clean()
        {
            File.Delete(testFile);
        }

        [TestMethod]
        public void SaveTest()
        {
            var saver = new ImportInformationCsvSaver(testFile);
            var list = new List<ImportInformation>(infos);
            var result = saver.Save(list);
            Assert.IsTrue(result.IsSuccess);
            var expectedLines = File.ReadAllLines("TestAssets\\Csv\\info.csv");
            var actualLines = File.ReadAllLines(testFile);
            Assert.AreEqual(expectedLines.Length, actualLines.Length);
            for(int i = 0; i < expectedLines.Length; i++)
            {
                
                Assert.AreEqual(expectedLines[i], actualLines[i]);
            }
        }
    }
}
