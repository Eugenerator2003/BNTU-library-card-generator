using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Models;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Models.Xlsx
{
    [TestClass]
    public class XlsxExportedStudentInformationTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            string lastname = "Petrov";
            string firstname = "Ivan";
            string patronymic = "Vladislavovich";
            DateTime birthDay = DateTime.Now;
            string passportNumber = "AC603636080";
            string passportInfo = "ROVD Misk";
            Person.SexType sex = Person.SexType.Male;
            var actual = new XlsxExportedStudentInformation(lastname, firstname, patronymic, birthDay, passportNumber, passportInfo, sex);
            Assert.AreEqual(lastname, actual.Lastname);
            Assert.AreEqual(firstname, actual.Firstname);
            Assert.AreEqual(patronymic, actual.Patronymic);
            Assert.AreEqual(birthDay, actual.BirthDate);
            Assert.AreEqual(passportNumber, actual.PassportNumber);
            Assert.AreEqual(passportInfo, actual.PassportInfo);
            Assert.AreEqual(sex, actual.Sex);
        }
    }
}
