using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using Moq;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases
{
    [TestClass]
    public class TransformExportedInformationUseCaseTests
    {
        private TransformExportedInformationUseCase _useCase = null!;

        [TestInitialize]
        public void InitializeTest()
        {
            var professions = new ProfessionCollection();
            var specializations = new SpecializationCollection();

            var service = new InformationTransformationService(professions, specializations);

            _useCase = new TransformExportedInformationUseCase(service);
        }

        [TestMethod]
        public void TransformSuccessTest()
        {
            var exportedList = new List<ExportedInformation>();

            var mockStudent1 = new Mock<ExportedStudentInformation>(
                "Иванов",
                "Иван",
                "Иванович",
                new DateTime(2000, 1, 1),
                "1234 567890",
                "ОВД г. Минска",
                Person.SexType.Male);

            var mockContact1 = new Mock<ExportedContactInformation>(
                "Минск",
                new List<string> { "+375291234567" },
                "ivanov@example.com");

            mockContact1.Setup(c => c.GetAddress()).Returns("ул. Пушкина, д. 10");

            var mockStudy1 = new Mock<ExportedStudyInformation>(
                new DateTime(2023, 9, 1),
                new DateTime(2027, 6, 30),
                "Физический",
                "101",
                "ФЗ-101",
                StudyGroup.DepartmentType.Correspondence);

            exportedList.Add(new ExportedInformation(
                1,
                mockStudent1.Object,
                mockStudy1.Object,
                mockContact1.Object,
                "ivanov.jpg"));

            var mockStudent2 = new Mock<ExportedStudentInformation>(
                "Петрова",
                "Мария",
                "Алексеевна",
                new DateTime(2001, 5, 15),
                "4321 098765",
                "ОВД г. Минска",
                Person.SexType.Female);

            var mockContact2 = new Mock<ExportedContactInformation>(
                "Минск",
                new List<string> { "+375291112233" },
                "petrova@example.com");

            mockContact2.Setup(c => c.GetAddress()).Returns("пр. Независимости, д. 50");

            var mockStudy2 = new Mock<ExportedStudyInformation>(
                new DateTime(2023, 9, 1),
                new DateTime(2027, 6, 30),
                "Математический",
                "202",
                "МТ-202",
                StudyGroup.DepartmentType.Correspondence);

            exportedList.Add(new ExportedInformation(
                2,
                mockStudent2.Object,
                mockStudy2.Object,
                mockContact2.Object,
                "petrova.jpg"));

            var importingParameter = new ImportingParameter(DateTime.Now, 1000);

            var parameter = new TransformExportedInformationCaseParameter(exportedList, importingParameter);

            var result = _useCase.Transform(parameter);

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            Assert.IsNotNull(result.ImportInformation);


        }

        [TestMethod]
        public void TransformFailureTest()
        {
            var parameter = new TransformExportedInformationCaseParameter(null!, new ImportingParameter(DateTime.Now, 1000));

            var result = _useCase.Transform(parameter);

            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Message);
            Assert.IsNotNull(result.ImportInformation);
            Assert.AreEqual(0, result.ImportInformation.Count);
        }
    }

}
