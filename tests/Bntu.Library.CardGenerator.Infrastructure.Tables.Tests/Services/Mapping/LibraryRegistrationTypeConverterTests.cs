using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Services.Mapping
{
    [TestClass]
    public class LibraryRegistrationTypeConverterTests
    {
        [DataTestMethod]
        [DataRow(2025)]
        [DataRow(2050)]
        public void ConvertTest(int year)
        {
            var converter = new LibraryRegistrationTypeConverter();
            var registration = new LibraryRegistration(new DateTime(year, 1, 1));
            var actualResualt = converter.ConvertToString(registration, null!, null!);
            Assert.AreEqual(year.ToString(), actualResualt);
        }
    }
}
