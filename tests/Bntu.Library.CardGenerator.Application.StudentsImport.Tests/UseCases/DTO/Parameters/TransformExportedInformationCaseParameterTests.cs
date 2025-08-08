using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.UseCases.DTO.Parameters
{
    [TestClass]
    public class TransformExportedInformationCaseParameterTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var exportedList = new List<ExportedInformation>();
            var importingParameter = new ImportingParameter(new DateTime(2025, 1, 1), 100);

            var parameter = new TransformExportedInformationCaseParameter(exportedList, importingParameter);

            Assert.AreSame(exportedList, parameter.ExportedInformation);
            Assert.AreSame(importingParameter, parameter.ImportingParameter);
        }
    }
}
