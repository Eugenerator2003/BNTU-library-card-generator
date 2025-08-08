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
using Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Util;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases
{
    [TestClass]
    public class TransformExportedInformationUseCaseTests
    {
        private TransformExportedInformationUseCase _useCase = null!;
        private List<Profession> _professions = null!;
        private List<Specialization> _specializations = null!;

        [TestInitialize]
        public void InitializeTest()
        {
            _professions = InformationOperations.GetProfessions();
            _specializations = InformationOperations.GetSpecializations(_professions);

            var professionsCollection = InformationOperations.GetProfessionCollection(_professions);
            var specializationsCollection = InformationOperations.GetSpecializationCollection(_specializations);

            var service = new InformationTransformationService(professionsCollection, specializationsCollection);

            _useCase = new TransformExportedInformationUseCase(service);
        }

        [TestMethod]
        public void TransformSuccessTest()
        {
            var expectedList = new List<ExportedInformation>();
            for(int i = 0; i < 4000; i++)
            {
                expectedList.Add(InformationOperations.GetRandomExportedInformation(_specializations));
            }
            var importingParameter = new ImportingParameter(DateTime.Now, 1000);
            var parameter = new TransformExportedInformationCaseParameter(expectedList, importingParameter);

            var actualResult = _useCase.Transform(parameter);

            Assert.IsTrue(actualResult.Success);
            Assert.IsNotNull(actualResult.ImportInformation);
            for (int i = 0; i < 4000; i++)
            {
                var actual = actualResult.ImportInformation[i];
                InformationOperations.AssertingEquals(importingParameter.StartCodbar + i, expectedList[i], actual, _professions);
            }
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