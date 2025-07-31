using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.Services.Generator
{
    [TestClass]
    public class GeneratedReaderCardTests
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
            StudyGroup.DepartmentType department = Enum.Parse<StudyGroup.DepartmentType>(departmentS, true);

            GeneratedReaderCard card = new GeneratedReaderCard(codbar, pid, issueDate, expirationDate, lastname, firstname, facultyName, groupNumber, department);

            Assert.AreEqual(codbar, card.CodbarValue);
            Assert.AreEqual(pid, card.PId);
            Assert.AreEqual(issueDate, card.IssueDate);
            Assert.AreEqual(expirationDate, card.ExpirationDate);
            Assert.AreEqual(lastname, card.Lastname);
            Assert.AreEqual(firstname, card.Firstname);
            Assert.AreEqual(facultyName, card.Faculty);
            Assert.AreEqual(groupNumber, card.StudyGroup);
            Assert.AreEqual(department, card.Department);
        }

        [DataTestMethod]
        [DataRow]
        [DataRow]
        [DataRow]
        public void InitializeFromReaderCardTest()
        {
            var card = RandomValuesGenerator.GenerateRandomReaderCard();
            var generatedCard = new GeneratedReaderCard(card);

            Assert.AreEqual(card.CodbarValue, generatedCard.CodbarValue);
            Assert.AreEqual(card.PId, generatedCard.PId);
            Assert.AreEqual(card.IssueDate, generatedCard.IssueDate);
            Assert.AreEqual(card.ExpirationDate, generatedCard.ExpirationDate);
            Assert.AreEqual(card.Lastname, generatedCard.Lastname);
            Assert.AreEqual(card.Firstname, generatedCard.Firstname);
            Assert.AreEqual(card.Faculty, generatedCard.Faculty);
            Assert.AreEqual(card.StudyGroup, generatedCard.StudyGroup);
            Assert.AreEqual(card.Department, generatedCard.Department);
        }
    }
}
