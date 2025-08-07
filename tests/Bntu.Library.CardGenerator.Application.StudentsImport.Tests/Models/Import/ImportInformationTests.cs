using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Faculty = Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Faculty;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class ImportInformationTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var student = new ImportStudentInformation(
                Person.SexType.Male,
                "Романов",
                "Илья",
                new DateTime(2000, 1, 1),
                "MP0123456");

            var contact = new ImportContactInformation(
                "Минск",
                "ул. Независимости 23",
                new List<string> { "+375291234567" },
                "ilya.romanov@example.com");

            var faculty = new Faculty("Информационные технологии");
            var profession = new Profession(12, "Программист");
            var specialization = new Specialization(15, "Системное программирование", 12);

            var group = new ImportStudyGroup(
                faculty,
                profession,
                specialization,
                "ИТ-101",
                StudyGroup.DepartmentType.Correspondence,
                new DateTime(2025, 6, 30));

            var registration = new LibraryRegistration(new DateTime(2021, 9, 1));
            var codbar = 12345678;
            var photoname = "photo_ilya.jpg";

            var info = new ImportInformation(codbar, photoname, student, contact, group, registration);

            Assert.AreEqual(codbar, info.Codbar);
            Assert.AreEqual(photoname, info.Photoname);
            Assert.AreEqual(student, info.Student);
            Assert.AreEqual(contact, info.Contact);
            Assert.AreEqual(group, info.Group);
            Assert.AreEqual(registration, info.LibraryRegistration);
        }

        [TestMethod]
        public void EqualsTrueTest()
        {
            var student = new ImportStudentInformation(
                Person.SexType.Female,
                "Сидорова",
                "Анна",
                new DateTime(1999, 3, 15),
                "MP6543210");

            var contact = new ImportContactInformation(
                "Гомель",
                "ул. Ленина 5",
                new List<string> { "+375291111111" },
                "anna.sid@example.com");

            var faculty = new Faculty("Математика");
            var profession = new Profession(5, "Аналитик");
            var specialization = new Specialization(7, "Статистика", 5);

            var group = new ImportStudyGroup(
                faculty,
                profession,
                specialization,
                "МА-202",
                StudyGroup.DepartmentType.Correspondence,
                new DateTime(2024, 7, 1));

            var registration = new LibraryRegistration(new DateTime(2020, 10, 15));
            var codbar = 87654321;
            var photoname = "anna.jpg";

            var info1 = new ImportInformation(codbar, photoname, student, contact, group, registration);
            var info2 = new ImportInformation(codbar, photoname, student, contact, group, registration);

            Assert.IsTrue(info1.Equals(info2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            var student1 = new ImportStudentInformation(
                Person.SexType.Male,
                "Козлов",
                "Артём",
                new DateTime(1998, 11, 12),
                "MP777888");

            var student2 = new ImportStudentInformation(
                Person.SexType.Male,
                "Козлов",
                "Артём",
                new DateTime(1998, 11, 12),
                "MP777889"); // изменён номер паспорта

            var contact = new ImportContactInformation(
                "Витебск",
                "ул. Победы 17",
                new List<string> { "+375292222222" },
                "artem.kozlov@example.com");

            var faculty = new Faculty("Физика");
            var profession = new Profession(8, "Лаборант");
            var specialization = new Specialization(9, "Экспериментальная физика", 8);

            var group = new ImportStudyGroup(
                faculty,
                profession,
                specialization,
                "ФИ-301",
                StudyGroup.DepartmentType.Correspondence,
                new DateTime(2023, 12, 20));

            var registration = new LibraryRegistration(new DateTime(2019, 9, 1));
            var codbar = 10101010;
            var photoname = "kozlov.png";

            var info1 = new ImportInformation(codbar, photoname, student1, contact, group, registration);
            var info2 = new ImportInformation(codbar, photoname, student2, contact, group, registration);

            Assert.IsFalse(info1.Equals(info2));
        }

        [TestMethod]
        public void HashCodeSameTest()
        {
            var student = new ImportStudentInformation(
                Person.SexType.Male,
                "Иванов",
                "Виктор",
                new DateTime(2001, 4, 3),
                "MP0987654");

            var contact = new ImportContactInformation(
                "Могилев",
                "ул. Крупской 9",
                new List<string> { "+375293333333" },
                "viktor.ivanov@example.com");

            var faculty = new Faculty("Архитектура");
            var profession = new Profession(6, "Градостроитель");
            var specialization = new Specialization(6, "Городское планирование", 6);

            var group = new ImportStudyGroup(
                faculty,
                profession,
                specialization,
                "АР-404",
                StudyGroup.DepartmentType.Fulltime,
                new DateTime(2026, 5, 15));

            var registration = new LibraryRegistration(new DateTime(2022, 2, 2));
            var codbar = 22223333;
            var photoname = "ivanov.jpg";

            var info1 = new ImportInformation(codbar, photoname, student, contact, group, registration);
            var info2 = new ImportInformation(codbar, photoname, student, contact, group, registration);

            Assert.AreEqual(info1.GetHashCode(), info2.GetHashCode());
        }

        [TestMethod]
        public void HashCodeDifferentTest()
        {
            var student = new ImportStudentInformation(
                Person.SexType.Female,
                "Петрова",
                "Ольга",
                new DateTime(2002, 8, 8),
                "MP1111111");

            var contact = new ImportContactInformation(
                "Брест",
                "ул. Московская 3",
                new List<string> { "+375294444444" },
                "olga.petrova@example.com");

            var faculty = new Faculty("Экономика");
            var profession = new Profession(11, "Бухгалтер");
            var specialization = new Specialization(13, "Финансовый учёт", 11);

            var group = new ImportStudyGroup(
                faculty,
                profession,
                specialization,
                "ЭК-303",
                StudyGroup.DepartmentType.Correspondence,
                new DateTime(2027, 10, 10));

            var registration = new LibraryRegistration(new DateTime(2021, 3, 3));

            var info1 = new ImportInformation(999999, "olga.jpg", student, contact, group, registration);
            var info2 = new ImportInformation(999998, "olga.jpg", student, contact, group, registration); // изменён codbar

            Assert.AreNotEqual(info1.GetHashCode(), info2.GetHashCode());
        }
    }
}
