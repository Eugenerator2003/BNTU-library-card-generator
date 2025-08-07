using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using System;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class ImportStudentInformationTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var sex = Person.SexType.Male;
            var lastname = "Иванов";
            var givenName = "Иван";
            var birthDate = new DateTime(2000, 5, 10);
            var passportInfo = "AB1234567";

            var studentInfo = new ImportStudentInformation(sex, lastname, givenName, birthDate, passportInfo);

            Assert.AreEqual(sex, studentInfo.Sex);
            Assert.AreEqual(lastname, studentInfo.Lastname);
            Assert.AreEqual(givenName, studentInfo.GivenName);
            Assert.AreEqual(birthDate, studentInfo.BirthDate);
            Assert.AreEqual(passportInfo, studentInfo.PassportInfromation);
        }
    }
}
