using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
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
    public class DepartmentFormTypeConverterTests
    {
        [TestMethod]
        public void ConvertFulltimeTest()
        {
            var converter = new DepartmentFormTypeConverter();
            var department = StudyGroup.DepartmentType.Fulltime;
            var actualResualt = converter.ConvertToString(department, null!, null!);
            Assert.AreEqual("очная", actualResualt);
        }

        [TestMethod]
        public void ConvertCorrespondenceTest()
        {
            var converter = new DepartmentFormTypeConverter();
            var department = StudyGroup.DepartmentType.Correspondence;
            var actualResualt = converter.ConvertToString(department, null!, null!);
            Assert.AreEqual("заочная", actualResualt);
        }
    }
}
