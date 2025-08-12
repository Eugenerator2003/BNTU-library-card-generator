using Bntu.Library.CardGenerator.Domain.ValueObjects;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bntu.Library.CardGenerator.Domain.ValueObjects.StudyGroup;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Models.Xlsx
{
    [TestClass]
    public class XlsxExportedStudyInformationTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            DateTime studyStartDate = DateTime.Now.AddDays(-1);
            DateTime graduationDate = DateTime.Now.AddDays(1);
            string faculty = "fms-1000";
            string specialization = "3123141";
            string groupNumber = "134059000";
            DepartmentType department = DepartmentType.Correspondence;
            var actual = new XlsxExportedStudyInformation(studyStartDate, graduationDate, faculty, specialization, groupNumber, department);
            Assert.AreEqual(studyStartDate, actual.StudyStartDate);
            Assert.AreEqual(graduationDate, actual.GraduationDate);
            Assert.AreEqual(faculty, actual.Faculty);
            Assert.AreEqual(specialization, actual.Specialization);
            Assert.AreEqual(groupNumber, actual.GroupNumber);
            Assert.AreEqual(department, actual.Department);
        }

        [TestMethod]
        public void InitializeNullSpecializationTest()
        {
            DateTime studyStartDate = DateTime.Now.AddDays(100);
            DateTime graduationDate = DateTime.Now.AddDays(150);
            string faculty = "fitr-2030";
            string? specialization = null;
            string groupNumber = "5991413030";
            DepartmentType department = DepartmentType.Fulltime;
            var actual = new XlsxExportedStudyInformation(studyStartDate, graduationDate, faculty, specialization!, groupNumber, department);
            Assert.AreEqual(studyStartDate, actual.StudyStartDate);
            Assert.AreEqual(graduationDate, actual.GraduationDate);
            Assert.AreEqual(faculty, actual.Faculty);
            Assert.AreEqual("", actual.Specialization);
            Assert.AreEqual(groupNumber, actual.GroupNumber);
            Assert.AreEqual(department, actual.Department);
        }

        [TestMethod]
        public void InitializeSpecializationNameTest()
        {
            DateTime studyStartDate = DateTime.Now.AddDays(-100);
            DateTime graduationDate = DateTime.Now.AddDays(+100000);
            string faculty = "ftug-2025";
            string? specialization = "мемеджмент 1-666-666-66";
            string groupNumber = "group";
            DepartmentType department = DepartmentType.Fulltime;
            var actual = new XlsxExportedStudyInformation(studyStartDate, graduationDate, faculty, specialization!, groupNumber, department);
            Assert.AreEqual(studyStartDate, actual.StudyStartDate);
            Assert.AreEqual(graduationDate, actual.GraduationDate);
            Assert.AreEqual(faculty, actual.Faculty);
            Assert.AreEqual("1-666-666-66", actual.Specialization);
            Assert.AreEqual(groupNumber, actual.GroupNumber);
            Assert.AreEqual(department, actual.Department);
        }
    }
}
