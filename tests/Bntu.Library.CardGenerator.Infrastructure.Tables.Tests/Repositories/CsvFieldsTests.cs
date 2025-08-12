using Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Repositories
{
    [TestClass]
    public class CsvFieldsTests
    {
        [TestMethod]
        public void InitializeCurrentFieldTest()
        {
            CsvFields fields = new CsvFields(["field1", "field2"]);
            Assert.AreEqual("", fields.CurrentField());
        }

        [TestMethod]
        public void RoundFieldsTest()
        {
            CsvFields fields = new CsvFields(["field1", "field2"]);
            Assert.AreEqual("field1", fields.NextField());
        }

        [TestMethod]
        public void NextFieldTest()
        {
            CsvFields fields = new CsvFields(["field1", "field2", "field3"]);
            Assert.AreEqual("field1", fields.NextField());
            Assert.AreEqual("field2", fields.NextField());
            Assert.AreEqual("field3", fields.NextField());
            Assert.AreEqual("field1", fields.NextField());
        }
    }
}
