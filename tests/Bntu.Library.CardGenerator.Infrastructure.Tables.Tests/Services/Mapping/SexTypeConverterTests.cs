using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Services.Mapping
{
    [TestClass]
    public class SexTypeConverterTests
    {
        [TestMethod]
        public void ConvertMaleTest()
        {
            var converter = new SexTypeConverter();
            var sex = Person.SexType.Male;
            var actualResualt = converter.ConvertToString(sex, null!, null!);
            Assert.AreEqual("мужской", actualResualt);
        }

        [TestMethod]
        public void ConvertFemaleTest()
        {
            var converter = new SexTypeConverter();
            var sex = Person.SexType.Male;
            var actualResualt = converter.ConvertToString(sex, null!, null!);
            Assert.AreEqual("мужской", actualResualt);
        }
    }
}
