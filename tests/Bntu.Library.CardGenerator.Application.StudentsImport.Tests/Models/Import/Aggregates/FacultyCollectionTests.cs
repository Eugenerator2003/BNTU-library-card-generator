using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import.Aggregates
{
    [TestClass]
    public class FacultyCollectionTests
    {
        [TestMethod]
        public void AddUniqueFacultyTest()
        {
            var collection = new FacultyCollection();
            var faculty = new Faculty("Факультет математики");

            collection.Add(faculty);

            Assert.AreEqual(faculty, collection["Факультет математики"]);
        }

        [TestMethod]
        public void AddDuplicateFacultyThrowsTest()
        {
            var collection = new FacultyCollection();
            var faculty1 = new Faculty("Факультет информатики");
            var faculty2 = new Faculty("Факультет информатики");

            collection.Add(faculty1);

            Assert.ThrowsException<ArgumentException>(() => collection.Add(faculty2));
        }

        [TestMethod]
        public void IndexerReturnsNullIfNotFoundTest()
        {
            var collection = new FacultyCollection();

            var result = collection["Неизвестный факультет"];

            Assert.IsNull(result);
        }
    }
}
