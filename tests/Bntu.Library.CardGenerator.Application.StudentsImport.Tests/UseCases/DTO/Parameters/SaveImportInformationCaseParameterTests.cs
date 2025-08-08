using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Bntu.Library.CardGenerator.Application.Tests.StudentsImport.UseCases.DTO.Parameters
{
    [TestClass]
    public class SaveImportInformationCaseParameterTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var list = new List<ImportInformation>();

            var parameter = new SaveImportInformationCaseParameter(list);

            Assert.AreEqual(list, parameter.ImportInformation);
        }
    }
}
