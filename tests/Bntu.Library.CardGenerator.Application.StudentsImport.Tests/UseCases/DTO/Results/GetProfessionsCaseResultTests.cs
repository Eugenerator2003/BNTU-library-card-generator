using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bntu.Library.CardGenerator.Application.Tests.StudentsImport.UseCases.DTO.Results
{
    [TestClass]
    public class GetProfessionsCaseResultTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            bool success = true;
            string message = "Success";
            var professions = new ProfessionCollection();

            var result = new GetProfessionsCaseResult(success, message, professions);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(message, result.Message);
            Assert.AreSame(professions, result.Professions);
        }
    }
}
