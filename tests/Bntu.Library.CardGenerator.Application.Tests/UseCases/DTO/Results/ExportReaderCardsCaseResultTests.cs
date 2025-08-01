using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases.DTO.Results
{
    [TestClass]
    public class ExportReaderCardsCaseResultTests
    {
        [DataTestMethod]
        [DataRow(true, "message")]
        [DataRow(true, null)]
        [DataRow(false, "")]
        public void InitializeTest(bool success, string? message)
        {
            var actualResult = new ExportReaderCardsCaseResult(success, message);

            Assert.AreEqual(success, actualResult.IsSuccess);
            Assert.AreEqual(message ?? "", actualResult.Message);
        }
    }
}
