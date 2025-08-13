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

        [TestMethod]
        public void EqualsTrueTest()
        {
            var student1 = new XlsxExportedStudentInformation(
                lastname: "Иванов",
                firstname: "Егор",
                patronymic: "Алексеевич",
                birthDate: new DateTime(2010, 9, 23),
                passportNumber: "МР9264259",
                passportInfo: "ОВД г. Минска",
                sex: Person.SexType.Male
            );

            var student2 = new XlsxExportedStudentInformation(
                lastname: "Иванов",
                firstname: "Егор",
                patronymic: "Алексеевич",
                birthDate: new DateTime(2010, 9, 23),
                passportNumber: "МР9264259",
                passportInfo: "ОВД г. Минска",
                sex: Person.SexType.Male
            );

            Assert.IsTrue(student1.Equals(student2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            var student1 = new XlsxExportedStudentInformation(
                lastname: "Иванов",
                firstname: "Егор",
                patronymic: "Алексеевич",
                birthDate: new DateTime(2010, 9, 23),
                passportNumber: "МР9264259",
                passportInfo: "ОВД г. Минска",
                sex: Person.SexType.Male
            );

            var student2 = new XlsxExportedStudentInformation(
                lastname: "Сидоров",
                firstname: "Алексей",
                patronymic: "Андреевич",
                birthDate: new DateTime(2008, 11, 17),
                passportNumber: "ВН6771411",
                passportInfo: "ОВД г. Минска",
                sex: Person.SexType.Male
            );

            Assert.IsFalse(student1.Equals(student2));
        }
    }
}
