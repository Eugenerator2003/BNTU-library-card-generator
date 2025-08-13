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
    public class SpecializationConverterTests
    {
        [DataTestMethod]
        [DataRow("Специалист 12-321-503100")]
        [DataRow("Неспециалист 00-0000-000")]
        public void ConvertTest(string value)
        {
            var specialization = new Specialization(0, value);
            var conveter = new SpecializationConverter();
            var result = conveter.ConvertToString(specialization, null!, null!);
            Assert.AreEqual(value, result);
        }
    }
}
