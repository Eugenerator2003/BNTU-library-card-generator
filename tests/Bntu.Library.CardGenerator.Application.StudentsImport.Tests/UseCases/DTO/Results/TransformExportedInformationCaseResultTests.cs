using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases.DTO.Results
{
    [TestClass]
    public class TransformExportedInformationCaseResultTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var importList = new List<ImportInformation>
            {
                new ImportInformation(
                    codbar: 1001,
                    photoname: "photo.jpg",
                    student: new ImportStudentInformation(
                        set: Person.SexType.Male,
                        lastname: "Иванов",
                        givenName: "Иван Иванович",
                        birthDate: DateTime.Parse("2000-01-01"),
                        passportInfromation: "AB1234567 МВД РБ"),
                    contact: new ImportContactInformation(
                        city: "Минск",
                        address: "ул. Пушкина, д. 10",
                        phones: new List<string> { "+375291112233" },
                        email: "ivanov@example.com"),
                    group: new ImportStudyGroup(
                        faculty: new Faculty("ФИТР-2025"),
                        profession: null,
                        specialization: null,
                        number: "12345",
                        departmentType: Domain.ValueObjects.StudyGroup.DepartmentType.Correspondence,
                        graduationDate: DateTime.Parse("2025-09-01")),
                    libraryRegistration: new LibraryRegistration(DateTime.Parse("2024-09-01"))
                )
            };

            var result = new TransformExportedInformationCaseResult(true, "Успех", importList);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Успех", result.Message);
            Assert.AreSame(importList, result.ImportInformation);
        }
    }
}
