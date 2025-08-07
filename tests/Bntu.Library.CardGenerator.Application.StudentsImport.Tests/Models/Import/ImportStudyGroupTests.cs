using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using Faculty = Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Faculty;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class ImportStudyGroupTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var faculty = new Faculty("Механика времени");
            var profession = new Profession(1, "Хрономеханик");
            var specialization = new Specialization(2, "Петлевая инженерия", 1);
            var number = "ГР-314";
            var departmentType = StudyGroup.DepartmentType.Correspondence;
            var graduationDate = new DateTime(2124, 6, 30);

            var group = new ImportStudyGroup(faculty, profession, specialization, number, departmentType, graduationDate);

            Assert.AreEqual(faculty, group.Faculty);
            Assert.AreEqual(profession, group.Profession);
            Assert.AreEqual(specialization, group.Specialization);
            Assert.AreEqual(number, group.Number);
            Assert.AreEqual(departmentType, group.DepartmentType);
            Assert.AreEqual(graduationDate, group.GraduationDate);
        }

        [TestMethod]
        public void EqualsTrueTest()
        {
            var faculty = new Faculty("Космодинамика");
            var profession = new Profession(7, "Астронавигация");
            var specialization = new Specialization(3, "Траекторный анализ", 7);
            var number = "КО-007";
            var departmentType = StudyGroup.DepartmentType.Correspondence;
            var graduationDate = new DateTime(2099, 7, 1);

            var g1 = new ImportStudyGroup(faculty, profession, specialization, number, departmentType, graduationDate);
            var g2 = new ImportStudyGroup(faculty, profession, specialization, number, departmentType, graduationDate);

            Assert.IsTrue(g1.Equals(g2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            var faculty1 = new Faculty("Факультет иллюзий");
            var faculty2 = new Faculty("Факультет реальности");
            var profession = new Profession(1, "Иллюзионист");
            var specialization = new Specialization(1, "Оптические трюки", 1);
            var number = "И-001";
            var departmentType = StudyGroup.DepartmentType.Fulltime;
            var graduationDate = new DateTime(2030, 5, 10);

            var g1 = new ImportStudyGroup(faculty1, profession, specialization, number, departmentType, graduationDate);
            var g2 = new ImportStudyGroup(faculty2, profession, specialization, number, departmentType, graduationDate);

            Assert.IsFalse(g1.Equals(g2));
        }

        [TestMethod]
        public void HashCodeSameTest()
        {
            var faculty = new Faculty("Энергосфера");
            var profession = new Profession(3, "Реакторщик");
            var specialization = new Specialization(5, "Плазмовая генерация", 3);
            var number = "ЭН-456";
            var departmentType = StudyGroup.DepartmentType.Fulltime;
            var graduationDate = new DateTime(2044, 12, 1);

            var g1 = new ImportStudyGroup(faculty, profession, specialization, number, departmentType, graduationDate);
            var g2 = new ImportStudyGroup(faculty, profession, specialization, number, departmentType, graduationDate);

            Assert.AreEqual(g1.GetHashCode(), g2.GetHashCode());
        }

        [TestMethod]
        public void HashCodeDifferentTest()
        {
            var faculty = new Faculty("Материаловедение");
            var profession = new Profession(10, "Керамист");
            var specialization = new Specialization(11, "Термостойкие сплавы", 10);
            var number1 = "МТ-001";
            var number2 = "МТ-002";
            var departmentType = StudyGroup.DepartmentType.Fulltime;
            var graduationDate = new DateTime(2027, 9, 1);

            var g1 = new ImportStudyGroup(faculty, profession, specialization, number1, departmentType, graduationDate);
            var g2 = new ImportStudyGroup(faculty, profession, specialization, number2, departmentType, graduationDate);

            Assert.AreNotEqual(g1.GetHashCode(), g2.GetHashCode());
        }
    }
}
