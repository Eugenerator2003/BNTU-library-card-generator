using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Application.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Bntu.Library.CardGenerator.Application.Tests.StudentsImport.UseCases
{
    [TestClass]
    public class SaveImportInformationUseCaseTests
    {
        private Mock<IImportInformationSaver> _saverMock = null!;
        private SaveImportInformationUseCase _useCase = null!;

        [TestInitialize]
        public void InitializeTest()
        {
            _saverMock = new Mock<IImportInformationSaver>();
            _useCase = new SaveImportInformationUseCase(_saverMock.Object);
        }

        [TestMethod]
        public void SaveSuccessTest()
        {
            var importInfoList = new List<ImportInformation>();

            _saverMock.Setup(s => s.Save(importInfoList))
                .Returns(ApplicationResult<EmptyResult>.Success(new EmptyResult(), "Saved successfully"));

            var parameter = new SaveImportInformationCaseParameter(importInfoList);

            var result = _useCase.Save(parameter);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Saved successfully", result.Message);
        }

        [TestMethod]
        public void SaveFailureTest()
        {
            var importInfoList = new List<ImportInformation>();

            _saverMock.Setup(s => s.Save(importInfoList))
                .Returns(ApplicationResult<EmptyResult>.Error("Save failed"));

            var parameter = new SaveImportInformationCaseParameter(importInfoList);

            var result = _useCase.Save(parameter);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Save failed", result.Message);
        }
    }
}
