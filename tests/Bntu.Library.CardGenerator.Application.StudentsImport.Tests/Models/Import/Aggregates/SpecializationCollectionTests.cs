using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import.Aggregates
{
    [TestClass]
    public class SpecializationCollectionTests
    {
        [TestMethod]
        public void AddTest()
        {
            var collection = new SpecializationCollection();
            var spec = new Specialization(1, "Специальность 123-45");

            collection.Add(spec);

            Assert.AreEqual(spec, collection[1]);
            Assert.AreEqual(spec, collection["12345"]);
            Assert.AreEqual(spec, collection["123-45"]);
        }

        [TestMethod]
        public void AddDuplicateByIdTest()
        {
            var collection = new SpecializationCollection();
            var spec1 = new Specialization(1, "Специальность 123-45");
            var spec2 = new Specialization(1, "Специальность 456-78");

            collection.Add(spec1);

            Assert.ThrowsException<ArgumentException>(() => collection.Add(spec2));
        }

        [TestMethod]
        public void AddDuplicateByNameTest()
        {
            var collection = new SpecializationCollection();
            var spec1 = new Specialization(1, "Специальность 123-45");
            var spec2 = new Specialization(2, "Специальность 123-45");

            collection.Add(spec1);

            Assert.ThrowsException<ArgumentException>(() => collection.Add(spec2));
        }

        [TestMethod]
        public void IndexerIdNotFoundTest()
        {
            var collection = new SpecializationCollection();
            var result = collection[999];
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IndexerNumberNotFoundTest()
        {
            var collection = new SpecializationCollection();
            var result = collection["99999"];
            Assert.IsNull(result);
        }
    }
}
