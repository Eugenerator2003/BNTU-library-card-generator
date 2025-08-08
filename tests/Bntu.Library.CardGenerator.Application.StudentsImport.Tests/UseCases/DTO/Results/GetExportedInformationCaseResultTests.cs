using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases.DTO.Results
{
    [TestClass]
    public class GetExportedInformationCaseResultTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            bool success = true;
            string message = "Completed";
            var exportedInformation = new List<ExportedInformation>();

            var result = new GetExportedInformationCaseResult(success, message, exportedInformation);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(message, result.Message);
            Assert.AreSame(exportedInformation, result.ExportedInformation);
        }
    }
}
