using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases.DTO.Results
{
    [TestClass]
    public class GetSpecializationCaseResultTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            bool success = true;
            string message = "Success";
            var specializations = new SpecializationCollection();

            var result = new GetSpecializationCaseResult(success, message, specializations);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(message, result.Message);
            Assert.AreSame(specializations, result.Specializations);
        }
    }
}
