using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bntu.Library.CardGenerator.Application.Tests.StudentsImport.UseCases.DTO.Results
{
    [TestClass]
    public class SaveImportInformationCaseResultTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            bool success = false;
            string message = "Failed to save import information";

            var result = new SaveImportInformationCaseResult(success, message);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(message, result.Message);
        }
    }
}
