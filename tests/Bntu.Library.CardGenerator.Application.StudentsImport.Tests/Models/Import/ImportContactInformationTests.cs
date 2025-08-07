using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using System.Collections.Generic;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class ImportContactInformationTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var city = "Минск";
            var address = "ул. Советская, 13";
            var phones = new List<string> { "1234567", "9876543" };
            var email = "test@example.com";

            var contact = new ImportContactInformation(city, address, phones, email);

            Assert.AreEqual(city, contact.City);
            Assert.AreEqual(address, contact.Address);
            CollectionAssert.AreEqual(phones, contact.Phones);
            Assert.AreEqual(email, contact.Email);
        }

        [TestMethod]
        public void ConstructorShouldCopyPhoneListTest()
        {
            var phones = new List<string> { "111", "222" };
            var contact = new ImportContactInformation(null, null, phones, null);

            phones.Add("333");

            Assert.AreEqual(2, contact.Phones.Count);
        }
    }
}
