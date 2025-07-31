using Bntu.Library.CardGenerator.Domain.Entities;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.Tests.Entities
{
    [TestClass]
    public class ReaderCardTests
    {
        [DataTestMethod]
        [DataRow(1, 1, "10-01-2024", "29-06-2028", "Nikitov", "Valeriy", "TUG", "31231203", "fulltime", "31231.jpg")]
        [DataRow(2139, 589915, "28-02-2019", "21-03-2021", "Vladov", "Vadim", "ATF-2026", "130850329", "correspondence", @"d://assets/students/photos/3129390.jpg")]
        [DataRow(102930, 31729312, "01-09-2025", "30-06-2029", "Zhdanov", "Stanislav", "FMMP-25", "600393129", "fulltime", @"/student-photo/person31729312.png")]
        public void InitializeTest(int codbar, int pid, string issueDateS, 
                                   string expirationDateS, string lastname, string firstname, 
                                   string facultyName, string groupNumber, string departmentS,
                                   string photoName)
        {
            DateTime issueDate = DateTime.Parse(issueDateS);
            DateTime expirationDate = DateTime.Parse(expirationDateS);
            CardInfo cardInfo = new CardInfo(codbar, pid, issueDate, expirationDate);
            Faculty faculty = new Faculty(facultyName);

            StudyGroup.DepartmentType department = Enum.Parse<StudyGroup.DepartmentType>(departmentS, true);
            StudyGroup studyGroup = new StudyGroup(groupNumber, department);
            Student student = new Student(lastname, firstname, faculty, studyGroup);

            Photo photo = new Photo(photoName);
            ReaderCard readerCard = new ReaderCard(cardInfo, student, photo);

            Assert.AreEqual(codbar, readerCard.CodbarValue);
            Assert.AreEqual(pid, readerCard.PId);
            Assert.AreEqual(issueDate, readerCard.IssueDate);
            Assert.AreEqual(expirationDate, readerCard.ExpirationDate);
            Assert.AreEqual(lastname, readerCard.Lastname);
            Assert.AreEqual(firstname, readerCard.Firstname);
            Assert.AreEqual(facultyName, readerCard.Faculty);
            Assert.AreEqual(groupNumber, readerCard.StudyGroup);
            Assert.AreEqual(department, readerCard.Department);
            Assert.AreEqual(photoName, readerCard.PhotoName);
        }

        [DataTestMethod]
        [DataRow(1, 1, "10-01-2024", "29-06-2028", "Nikitov", "Valeriy", "TUG", "31231203", "fulltime", "31231.jpg")]
        [DataRow(2139, 589915, "28-02-2019", "21-03-2021", "Vladov", "Vadim", "ATF-2026", "130850329", "correspondence", @"d://assets/students/photos/3129390.jpg")]
        [DataRow(102930, 31729312, "01-09-2025", "30-06-2029", "Zhdanov", "Stanislav", "FMMP-25", "600393129", "fulltime", @"/student-photo/person31729312.png")]
        public void EqualTest(int codbar, int pid, string issueDateS,
                                   string expirationDateS, string lastname, string firstname,
                                   string facultyName, string groupNumber, string departmentS,
                                   string photoName)
        {
            DateTime issueDate = DateTime.Parse(issueDateS);
            DateTime expirationDate = DateTime.Parse(expirationDateS);
            CardInfo cardInfo = new CardInfo(codbar, pid, issueDate, expirationDate);
            Faculty faculty = new Faculty(facultyName);

            StudyGroup.DepartmentType department = Enum.Parse<StudyGroup.DepartmentType>(departmentS, true);
            StudyGroup studyGroup = new StudyGroup(groupNumber, department);
            Student student = new Student(lastname, firstname, faculty, studyGroup);

            Photo photo = new Photo(photoName);
            ReaderCard readerCard = new ReaderCard(cardInfo, student, photo);
            ReaderCard readerCard2 = new ReaderCard(cardInfo, student, photo);

            Assert.AreEqual(readerCard, readerCard2);
        }
    }
}
