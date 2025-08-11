using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Application.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases
{
    [TestClass]
    public class GetSpecializationsUseCaseTests
    {
        private Mock<ISpecializationService> _serviceMock = null!;
        private GetSpecializationsUseCase _useCase = null!;

        [TestInitialize]
        public void InitializeTest()
        {
            _serviceMock = new Mock<ISpecializationService>();
            _useCase = new GetSpecializationsUseCase(_serviceMock.Object);
        }

        [TestMethod]
        public void GetSpecializationsSuccessTest()
        {
            var specializations = new SpecializationCollection();

            _serviceMock.Setup(s => s.GetSpecializations())
                .Returns(ApplicationResult<SpecializationCollection>.Success(specializations, "Success"));

            var result = _useCase.GetSpecializations();

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Success", result.Message);
            Assert.AreSame(specializations, result.Specializations);
        }

        [TestMethod]
        public void GetSpecializationsFailureTest()
        {
            var professions = new ProfessionCollection();

            _serviceMock.Setup(s => s.GetSpecializations())
                .Returns(ApplicationResult<SpecializationCollection>.Error("Error"));

            var result = _useCase.GetSpecializations();

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Error", result.Message);
            Assert.IsNotNull(result.Specializations);
        }
    }
}
