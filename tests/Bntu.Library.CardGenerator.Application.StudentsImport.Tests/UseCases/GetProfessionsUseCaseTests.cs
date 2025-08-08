using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Application.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases
{
    [TestClass]
    public class GetProfessionsUseCaseTests
    {
        private Mock<IProfessionService> _serviceMock = null!;
        private GetProfessionsUseCase _useCase = null!;

        [TestInitialize]
        public void InitializeTest()
        {
            _serviceMock = new Mock<IProfessionService>();
            _useCase = new GetProfessionsUseCase(_serviceMock.Object);
        }

        [TestMethod]
        public void GetProfessionsSuccessTest()
        {
            var professions = new ProfessionCollection();
            _serviceMock.Setup(s => s.GetProfessions())
                .Returns(ApplicationResult<ProfessionCollection>.Success(professions, "Success"));

            var result = _useCase.GetProfessions();

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Success", result.Message);
            Assert.AreSame(professions, result.Professions);
        }

        [TestMethod]
        public void GetProfessionsFailureTest()
        {
            _serviceMock.Setup(s => s.GetProfessions())
                .Returns(ApplicationResult<ProfessionCollection>.Error("Error"));

            var result = _useCase.GetProfessions();

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Error", result.Message);
            Assert.IsNotNull(result.Professions);
        }
    }
}
