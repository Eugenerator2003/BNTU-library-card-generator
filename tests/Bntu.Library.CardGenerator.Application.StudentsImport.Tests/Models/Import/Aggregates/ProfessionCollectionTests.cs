using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import.Aggregates
{
    [TestClass]
    public class ProfessionCollectionTests
    {
        [TestMethod]
        public void AddTest()
        {
            var collection = new ProfessionCollection();
            var profession = new Profession(1, "Информатика");

            collection.Add(profession);

            Assert.AreEqual(profession, collection[1]);
        }

        [TestMethod]
        public void AddDuplicateByIdTest()
        {
            var collection = new ProfessionCollection();
            var profession1 = new Profession(1, "Информатика");
            var profession2 = new Profession(1, "Экономика");

            collection.Add(profession1);

            Assert.ThrowsException<ArgumentException>(() => collection.Add(profession2));
        }

        [TestMethod]
        public void AddDuplicateByNameTest()
        {
            var collection = new ProfessionCollection();
            var profession1 = new Profession(1, "Информатика");
            var profession2 = new Profession(2, "Информатика");

            collection.Add(profession1);

            Assert.ThrowsException<ArgumentException>(() => collection.Add(profession2));
        }

        [TestMethod]
        public void IndexerNotFoundTest()
        {
            var collection = new ProfessionCollection();
            var result = collection[999];
            Assert.IsNull(result);
        }
    }
}
