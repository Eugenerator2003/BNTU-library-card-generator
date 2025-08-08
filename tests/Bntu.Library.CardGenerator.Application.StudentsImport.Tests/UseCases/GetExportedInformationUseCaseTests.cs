using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Application.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases
{
    [TestClass]
    public class GetExportedInformationUseCaseTests
    {
        private Mock<IExportedInformationService> _serviceMock = null!;
        private GetExportedInformationUseCase _useCase = null!;

        [TestInitialize]
        public void Initialize()
        {
            _serviceMock = new Mock<IExportedInformationService>();
            _useCase = new GetExportedInformationUseCase(_serviceMock.Object);
        }

        [TestMethod]
        public void GetExportedInformationSuccessTest()
        {
            var exportedList = new List<ExportedInformation>();
            _serviceMock.Setup(s => s.GetExportedInformation())
                .Returns(ApplicationResult<IList<ExportedInformation>>.Success(exportedList, "Success"));

            var result = _useCase.GetExportedInformation();

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Success", result.Message);
            Assert.AreSame(exportedList, result.ExportedInformation);
        }

        [TestMethod]
        public void GetExportedInformationFailureTest()
        {
            _serviceMock.Setup(s => s.GetExportedInformation())
                .Returns(ApplicationResult<IList<ExportedInformation>>.Error("Error"));

            var result = _useCase.GetExportedInformation();

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Error", result.Message);
            Assert.IsNotNull(result.ExportedInformation);
            Assert.AreEqual(0, result.ExportedInformation.Count);
        }
    }
}
