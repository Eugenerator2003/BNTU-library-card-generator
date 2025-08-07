using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using System;
using System.Collections.Generic;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Domain.ValueObjects;

namespace Bntu.Library.CardGenerator.Application.Tests.StudentsImport.Services
{
    [TestClass]
    public class InformationTransformationTests
    {
        [TestMethod]
        public void TransformSuccessTest()
        {
            var studentMock = new Mock<ExportedStudentInformation>(
                "Иванов",
                "Иван",
                "Иванович",
                new DateTime(2000, 1, 1),
                "1234 567890",
                "Выдан ...",
                Person.SexType.Male);

            var contactMock = new Mock<ExportedContactInformation>(
                "Минск",
                new List<string> { "+375291234567" },
                "ivanov@example.com");

            var studyMock = new Mock<ExportedStudyInformation>(
                new DateTime(2025, 9, 1),
                new DateTime(2027, 6, 30),
                "Факультет",
                "01-01",
                "ГП-01",
                StudyGroup.DepartmentType.Fulltime);

            var exportedInfo = new ExportedInformation(
                1,
                studentMock.Object,
                studyMock.Object,
                contactMock.Object,
                "photo.jpg");

            var professions = new ProfessionCollection();
            var specializations = new SpecializationCollection();

            var service = new InformationTransformationService(professions, specializations);

            var importParam = new ImportingParameter(DateTime.Now, 3133);
            var result = service.Transform(new List<ExportedInformation> { exportedInfo }, importParam);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.Value!.Count);
            Assert.AreEqual(importParam.StartCodbar, result.Value[0].Codbar);
            Assert.AreEqual("photo.jpg", result.Value[0].Photoname);
            Assert.AreEqual("Иванов", result.Value[0].Student.Lastname);
        }

        [TestMethod]
        public void TransformErrorTest()
        {
            var professions = new ProfessionCollection();
            var specializations = new SpecializationCollection();

            var service = new InformationTransformationService(professions, specializations);

            var importParam = new ImportingParameter(DateTime.Now, 3000);

            IList<ExportedInformation> exportedInformation = null!;

            var result = service.Transform(exportedInformation!, importParam);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsOperationError);
            Assert.IsTrue(result.Message.Contains("System.NullReferenceException"));
        }
    }
}
