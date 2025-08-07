using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using Faculty = Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Faculty;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import.Aggregates
{
    [TestClass]
    public class GroupCollectionTests
    {
        [TestMethod]
        public void AddTest()
        {
            var collection = new GroupCollection();
            var group = new ImportStudyGroup(
                new Faculty("ФИТР"),
                null,
                null,
                "101",
                StudyGroup.DepartmentType.Fulltime,
                new DateTime(2026, 6, 30)
            );

            collection.Add(group);

            Assert.AreEqual(group, collection["101"]);
        }

        [TestMethod]
        public void AddDuplicateTest()
        {
            var collection = new GroupCollection();
            var group = new ImportStudyGroup(
                new Faculty("ФИТР"),
                null,
                null,
                "101",
                StudyGroup.DepartmentType.Correspondence,
                new DateTime(2026, 6, 30)
            );

            collection.Add(group);

            Assert.ThrowsException<ArgumentException>(() => collection.Add(group));
        }

        [TestMethod]
        public void IndexerNotFoundTest()
        {
            var collection = new GroupCollection();
            var result = collection["999"];
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResetTest()
        {
            var collection = new GroupCollection();
            var group = new ImportStudyGroup(
                new Faculty("ФИТР"),
                null,
                null,
                "101",
                StudyGroup.DepartmentType.Correspondence,
                new DateTime(2026, 6, 30)
            );

            collection.Add(group);
            collection.Reset();

            Assert.IsNull(collection["101"]);
        }
    }
}
