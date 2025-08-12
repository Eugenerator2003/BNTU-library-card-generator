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
    public class ContactPhonesConverterTest
    {
        [TestMethod]
        public void ConvertEmptyPhoneListTest()
        {
            var converter = new ContactPhonesConverter();
            var contact = new ImportContactInformation("", "", ["+3753123124", "+3123883"], "");
            var actualResualt = converter.ConvertToString(contact, null!, null!);
            Assert.AreEqual("+3753123124,+3123883", actualResualt);
        }

        [TestMethod]
        public void ConvertTest()
        {
            var converter = new ContactPhonesConverter();
            var contact = new ImportContactInformation("", "", [], "");
            var actualResualt = converter.ConvertToString(contact, null!, null!);
            Assert.AreEqual("", actualResualt);
        }
    }
}
