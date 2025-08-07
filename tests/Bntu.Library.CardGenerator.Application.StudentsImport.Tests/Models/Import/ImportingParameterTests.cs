using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using System;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class ImportingParameterTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var currentYear = new DateTime(2025, 1, 1);
            var startCodbar = 1000;

            var parameter = new ImportingParameter(currentYear, startCodbar);

            Assert.AreEqual(currentYear, parameter.CurrentYear);
            Assert.AreEqual(startCodbar, parameter.StartCodbar);
        }

        [TestMethod]
        public void EqualsTrueTest()
        {
            var year = new DateTime(2025, 1, 1);
            var codbar = 1000;

            var param1 = new ImportingParameter(year, codbar);
            var param2 = new ImportingParameter(year, codbar);

            Assert.IsTrue(param1.Equals(param2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            var param1 = new ImportingParameter(new DateTime(2025, 1, 1), 1000);
            var param2 = new ImportingParameter(new DateTime(2024, 1, 1), 2000);

            Assert.IsFalse(param1.Equals(param2));
        }

        [TestMethod]
        public void GetHashCodeSameTest()
        {
            var year = new DateTime(2025, 1, 1);
            var codbar = 1000;

            var param1 = new ImportingParameter(year, codbar);
            var param2 = new ImportingParameter(year, codbar);

            Assert.AreEqual(param1.GetHashCode(), param2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCodeDifferentTest()
        {
            var param1 = new ImportingParameter(new DateTime(2025, 1, 1), 1000);
            var param2 = new ImportingParameter(new DateTime(2024, 1, 1), 2000);

            Assert.AreNotEqual(param1.GetHashCode(), param2.GetHashCode());
        }
    }
}
