using Bntu.Library.CardGenerator.Infrastructure.Tables.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Util
{
    [TestClass]
    public class XlsxOperationsTests
    {
        [DataTestMethod]
        [DataRow("A", 0)]
        [DataRow("AA", 26)]
        [DataRow("ba", 52)]
        [DataRow("Qq", 17 * 26 + 17 - 1)]
        public void ColumnLetterToIndexTest(string column, int value)
        {
            var actual = XlsxOperations.ColumnLetterToIndex(column);
            Assert.AreEqual(value, actual);
        }
    }
}
