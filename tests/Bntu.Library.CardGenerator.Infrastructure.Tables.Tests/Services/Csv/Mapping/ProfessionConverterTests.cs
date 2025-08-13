using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Services.Csv.Mapping
{
    [TestClass]
    public class ProfessionConverterTests
    {
        [DataTestMethod]
        [DataRow("Профессионал 5-031-31-0455")]
        [DataRow("Непрофессионал 1-34595-103")]
        public void ConvertTest(string value)
        {
            var profession = new Profession(0, value);
            var converter = new ProfessionConverter();
            var actual = converter.ConvertToString(profession, null!, null!);
            Assert.AreEqual(value, actual);
        }
    }
}
