using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using System;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class LibraryRegistrationTests
    {
        [TestMethod]
        public void ConstructorInitializeTest()
        {
            var date = new DateTime(2025, 8, 7);
            var registration = new LibraryRegistration(date);
            Assert.AreEqual(date, registration.RegistrationDate);
        }
    }
}
